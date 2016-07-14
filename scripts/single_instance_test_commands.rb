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
  driver.get('http://output.jsbin.com/bobiba/')

  puts 'finding label..'
  element = driver.find_element(:id, 'joanna')

  puts 'comparing text..'
  if element.text != 'Simple text'
    $stderr.puts "Element text not matching exectation: #{element.text}"
    exit 1
  end

  puts 'entering text..'
  driver.find_element(:name, 'luna').send_keys('some text')

  puts 'clicking link..'
  driver.find_element(:link_text, 'Goto google').click

  puts 'comparing target page..'
  matches = driver.find_elements(:tag_name, 'input')
  if matches.length == 0
    $stderr.puts "no inputs found, can't be google search page!"
    exit 1
  end

  search_btn = matches.find { |element| element.attribute('name') == 'btnK' }
  search_text = search_btn.attribute('value')
  if search_text != 'Google Search'
    $stderr.puts "Search button text not what was expected: #{search_text}"
    exit 1
  end

  puts 'done.'
ensure
  driver.quit unless driver.nil?
end
