define('vm.dashboard.page', ['knockback','ko', 'user.settings'],
    function (kb, ko, userSettings) {

        var isVisible = ko.observable(false);
        
        function onVisible() {
            isVisible(true);
        }

        return {
            isVisible: isVisible,
            onVisible: onVisible,
             currentUser: userSettings.currentUser
        };
    });