import { AppFarmaDashboardAdminPage } from './app.po';

describe('app-farma-dashboard-admin App', () => {
  let page: AppFarmaDashboardAdminPage;

  beforeEach(() => {
    page = new AppFarmaDashboardAdminPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!!');
  });
});
