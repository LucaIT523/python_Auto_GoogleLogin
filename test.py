from selenium import webdriver
from selenium.webdriver.common.action_chains import ActionChains
from selenium.webdriver.common.by import By
import time

# Set up the WebDriver
chrome_options = webdriver.ChromeOptions()
chrome_options.add_argument("--remote-debugging-port=9222")
chrome_options.add_argument("--no-first-run")
chrome_options.add_argument("--no-default-browser-check")
driver = webdriver.Chrome(options=chrome_options)

# Open the Chrome extensions page
driver.get('chrome://extensions/')
time.sleep(2)  # Wait for the page to load

# Path to the CRX file
crx_path = 'C:/test.crx'

# Find the element to drag the file to
drop_area = driver.find_element(By.ID, 'in-dev-mode')

# Perform the drag and drop action
action_chains = ActionChains(driver)
action_chains.click_and_hold(drop_area).perform()

# Simulate dragging the CRX file
action_chains.move_to_element_with_offset(drop_area, 10, 10).perform()
time.sleep(1)  # Wait a bit to simulate the drag

# Drop the file
action_chains.release().perform()

# Allow some time for the extension to be installed
time.sleep(5)

# Close the driver
driver.quit()
