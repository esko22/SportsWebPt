define('vm.master.page', ['vm.splash', 'vm.examine.page'],
    function (splashPage, examinePage) {

        return {
            splashPage: splashPage,
            examinePage : examinePage
        };
    });