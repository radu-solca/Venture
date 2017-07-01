import { VenturePage } from './app.po';

describe('venture App', () => {
  let page: VenturePage;

  beforeEach(() => {
    page = new VenturePage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!!');
  });
});
