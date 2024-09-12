import os
import time
import random
import simpleaudio as sa
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.common.exceptions import TimeoutException, NoSuchElementException
from datetime import datetime

# Be sure to use Spyder with selenium and simpleaudio installed by using
# pip install simpleaudio
# pip install selenium
# These two commands above in the console/terminal

# VARIABLES
min_time_between_checks = 15  # Time between website checks (max time is min+5) #DO NOT SET BELOW 10
min_percentage_for_notification = 10  # Percentage of discount for a notification
min_price = 2.00  # Minimum price for notification to filter out cheap ass skins
audioOn = True  # Turn off the "ding" by changing "True" to "False" and vice versa

# Get the directory of the current script file
script_dir = os.path.dirname(os.path.abspath(__file__))

# Define the path to the audio file
audio_file_path = os.path.join(script_dir, 'ding.wav')
current_time = datetime.now().strftime('%D:%H:%M:%S')
def play_audio(audio_file_path):
    if audioOn:
        # Play the audio file
        wave_obj = sa.WaveObject.from_wave_file(audio_file_path)
        play_obj = wave_obj.play()
        play_obj.wait_done()  # Wait until sound has finished playing
    
def getItemPercentage(driver):
    with open("SkinHistory.txt", "a", encoding='utf-8') as f:
        current_time = datetime.now().strftime('%D:%H:%M:%S')
        print(f"\n{current_time}")
        csfloatNewLink = "https://csfloat.com/search?sort_by=most_recent&type=buy_now"
        
        driver.get(csfloatNewLink)
    
    try:
        # Wait for the item cards to be present
        item_cards = WebDriverWait(driver, 20).until(
            EC.presence_of_all_elements_located((By.CSS_SELECTOR, 'item-card'))
        )
        
        items = []
        for item in item_cards[:10]:
            item_info = extractItemInfo(item)
            if item_info:
                items.append(item_info)
        
        return items
    except TimeoutException as e:
        print("TimeoutException:", e)
    except NoSuchElementException as e:
        print("NoSuchElementException:", e)
    return []

def extractItemInfo(item):
    try:
        # Find all path elements with specific stroke attribute
        path_elems = item.find_elements(By.CSS_SELECTOR, 'path[stroke="#FD484A"]')
        if path_elems:
            # Skip items with specific path elements
            return None
        
        # Find all circle elements
        circle_elems = item.find_elements(By.TAG_NAME, 'circle')
        valid_circle = False
        
        # Check if any circle element has the correct fill color
        for circle in circle_elems:
            fill_color = circle.get_attribute('fill')
            if fill_color and fill_color.lower() == '#64ec42':  # Ensure case-insensitive comparison
                valid_circle = True
                break
        
        if valid_circle:
            percentage_span = item.find_element(By.CSS_SELECTOR, 'span.percentage.ng-star-inserted')
            if percentage_span:
                percentage_text = percentage_span.text.strip().rstrip('%').lstrip('>')
                try:
                    # Clean the percentage text and convert to float
                    percentage_text = percentage_text.replace(',', '').replace(' ', '')
                    percentage_value = float(percentage_text)
                    if percentage_value >= min_percentage_for_notification:
                        item_name_elem = item.find_element(By.CSS_SELECTOR, '.item-name')
                        item_price_elem = item.find_element(By.CSS_SELECTOR, '.price')
                        item_name = item_name_elem.text.strip()
                        item_price = item_price_elem.text.strip()
                        item_price_text = item_price_elem.text.strip().lstrip('$').replace(',', '')
                        price_value = float(item_price_text)
                        if price_value >= min_price:
                            return {'name': item_name, 'price': item_price, 'percentage': percentage_text}
                except ValueError:
                    print(f"Could not convert percentage text to float: {percentage_text}")
    except NoSuchElementException:
        pass
    return None

if __name__ == "__main__":
    while True:
        try:
            # Initialize Chrome options
            chrome_options = webdriver.ChromeOptions()
            chrome_options.add_argument("--window-position=-10000,-10000")  # This will position the window off-screen
            
            driver = webdriver.Chrome(options=chrome_options)
            
            items = getItemPercentage(driver)
            
            item_found = False
            with open("SkinHistory.txt", "a", encoding='utf-8') as f:
                current_time = datetime.now().strftime('%D:%H:%M:%S')
                for item in items:
                    if item:
                        print(f"Item: {item['name']}, Price: {item['price']}, Percentage: {item['percentage']}")
                        f.write(current_time + "\n")
                        f.write(f"Item: {item['name']}, Price: {item['price']}, Percentage: {item['percentage']}\n")
                        item_found = True
                #f.write("\n")
                # Play sound only if an item was printed
                if item_found:
                    try:
                        play_audio(audio_file_path)
                    except Exception as e:
                        print(f"Error playing sound: {e}")
                else:
                    #f.write("No Items Found\n")
                    print("No Items Found")
            sleep_time = random.uniform(min_time_between_checks, min_time_between_checks + 5)
            time.sleep(sleep_time)
            driver.quit()
            
        except KeyboardInterrupt:
            print("Script interrupted by user.")
            break
        except Exception as e:
            print(f"An error occurred: {e}")
