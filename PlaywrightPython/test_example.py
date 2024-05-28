import re
from playwright.sync_api import Page, expect

def test_should_click_button(page: Page):
    page.goto('data:text/html,<script>var result;</script><button onclick="result=\'Clicked\'">Go</button>')
    page.locator('button').click()
    result = page.evaluate('result')
    assert result == 'Clicked'

def test_should_check_the_box(page: Page):
    page.set_content('<input id="checkbox" type="checkbox"></input>')
    page.locator('input').check()
    is_checked = page.evaluate('() => window.checkbox.checked')
    assert is_checked is True

def test_should_search_wiki(page: Page):
    page.goto('https://www.wikipedia.org/')
    page.select_option('#searchLanguage', 'en')
    page.locator('input[name="search"]').click()
    page.locator('input[name="search"]').fill('Playwright (software)')
    page.locator('input[name="search"]').press('Enter')
    page.wait_for_url('https://en.wikipedia.org/wiki/Playwright_(software)')
    expect(page).to_have_url('https://en.wikipedia.org/wiki/Playwright_(software)')
