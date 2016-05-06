require 'selenium-webdriver'

# This script just helps excercise some of the most common functionality
# in the driver

browser = {
  url: 'http://localhost:4444/',
  desired_capabilities: Selenium::WebDriver::Remote::Capabilities.firefox
}

begin
  puts 'creating driver..'
  driver = Selenium::WebDriver::Driver.for(:remote, browser)

  puts 'navigating to google..'
  driver.get('http://google.com')

  puts 'finding search box..'
  element = driver.find_element(:name, 'q')

  puts 'searching..'
  element.send_keys('semla')

  puts 'committing search..'
  element.submit
  sleep 2

  puts 'getting matches..'
  matches = driver.find_elements(:tag_name, 'h3')
  if matches.length == 0
    $stderr.puts "no matches for found!"
    exit 1
  end
  text = matches[0].text.downcase

  puts 'comparing matches..'
  if !text.include?('semla')
    $stderr.puts "match does not include expected text: #{text}"
    exit 1
  end

  puts 'done.'
ensure
  driver.quit unless driver.nil?
end
