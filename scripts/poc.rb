require 'selenium-webdriver'

# wraps the original remote bridge to allow to reconnect to a previously
# created bridge session
class KeepAliveBridge < Selenium::WebDriver::Remote::Bridge
  def initialize(mode, opts = {})
    if mode == :new
      self.server_url = opts[:url]
      super(opts)
    elsif mode == :connect
      connect_to(opts)
    else
      fail 'Allowed modes are connect or new: ' + mode
    end
  end

  def server_url=(url)
    @server_url = URI.parse(url)
    @server_url.path += '/' unless @server_url.path =~ %r{/\/$/}
  end

  attr_reader :server_url

  def connect_to(opts = {})
    opts = opts.dup
    @http = opts.delete(:http_client) do
      Selenium::WebDriver::Remote::Http::Default.new
    end

    self.server_url = opts[:server_url]
    @http.server_url = @server_url
    @capabilities = opts[:caps]
    @session_id = opts[:session_id]
  end

  private :connect_to

  # gets the connection info to use for connecting to the driver again
  def connection_info
    {
      session_id: @session_id,
      caps: @capabilities,
      server_url: @server_url.to_s
    }
  end
end

browser = [
  {
    url: 'http://localhost:9999/',
    desired_capabilities: Selenium::WebDriver::Remote::Capabilities.firefox
  },
  {
    url: 'http://localhost:9999/',
    desired_capabilities: Selenium::WebDriver::Remote::Capabilities.internet_explorer
  },
  {
    url: 'http://localhost:9999/',
    desired_capabilities: Selenium::WebDriver::Remote::Capabilities.chrome
  }
]

browser_sessions = []
browser.each do |caps|
  browser = caps[:desired_capabilities][:browser_name]
  puts "trying out #{browser}.."
  bridge = KeepAliveBridge.new(:new, caps)
  driver = Selenium::WebDriver::Driver.new(bridge)
  driver.get('http://google.com')
  browser_sessions.push({
    browser: browser,
    conn_info: bridge.connection_info
  })
  puts "#{browser} ran successfully."
end

browser_sessions.each do |session|
  browser_name = session[:browser]
  puts "closing #{browser_name}.."
  bridge = KeepAliveBridge.new(:connect, session[:conn_info])
  driver = Selenium::WebDriver::Driver.new(bridge)
  driver.quit
  puts "closed #{browser_name}."
end

puts 'all done.'
