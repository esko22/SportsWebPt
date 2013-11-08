define('vm.splash', ['ko'],
    function (ko) {

        var isVisible = ko.observable(false);


        return {
            isVisible: isVisible
        };
    });