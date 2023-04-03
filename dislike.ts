setInterval(() => {
  setInterval(() => window.scrollTo(0, document.body.scrollHeight), 10000);

  for (const doc of document.querySelectorAll<HTMLButtonElement>(
    'div[data-testid="unlike"]'
  )) {
    doc.click();
  }
}, 1000);
