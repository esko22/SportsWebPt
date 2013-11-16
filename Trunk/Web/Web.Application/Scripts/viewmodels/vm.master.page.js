define('vm.master.page', ['vm.splash', 'vm.examine.page', 'vm.research.page', 'vm.dashboard.page'],
    function (splashPage, examinePage, researchPage, dashboardPage) {

        return {
            splashPage: splashPage,
            examinePage: examinePage,
            researchPage: researchPage,
            dashboardPage: dashboardPage
        };
    });