import { test, expect } from '@playwright/test';

test('shouldClickButton', async ({ page }) => {
  await page.goto('data:text/html,<script>var result;</script><button onclick="result=\'Clicked\'">Go</button>');
  await page.locator('button').click();
  const result = await page.evaluate(() => (window as any).result);
  expect(result).toBe('Clicked');
});

test('shouldCheckTheBox', async ({ page }) => {
  await page.setContent('<input id="checkbox" type="checkbox"></input>');
  await page.locator('input').check();
  const isChecked = await page.evaluate(() => (document.getElementById('checkbox') as HTMLInputElement).checked);
  expect(isChecked).toBe(true);
});

test('shouldSearchWiki', async ({ page }) => {
  await page.goto('https://www.wikipedia.org/');
  await page.selectOption('#searchLanguage', 'en');
  await page.locator('input[name="search"]').click();
  await page.locator('input[name="search"]').fill('Playwright (software)');
  await page.locator('input[name="search"]').press('Enter');
  expect(page.url()).toBe('https://en.wikipedia.org/wiki/Playwright_(software)');
});
