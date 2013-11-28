define('vm.master.page', ['vm.splash', 'vm.examine.page', 'vm.research.page', 'vm.dashboard.page', 'config'],
    function (splashPage, examinePage, researchPage, dashboardPage,config) {

        return {
            splashPage: splashPage,
            examinePage: examinePage,
            researchPage: researchPage,
            dashboardPage: dashboardPage,
            templateIds : config.templateIds
        };
    });