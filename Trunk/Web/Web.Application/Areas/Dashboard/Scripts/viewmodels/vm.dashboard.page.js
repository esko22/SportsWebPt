define('vm.dashboard.page', ['knockback', 'user.settings'],
    function (kb, userSettings) {
        return { currentUser: userSettings.currentUser };
    });