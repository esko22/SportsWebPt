define('vm.master.header',
['ko', 'config', 'model.user', 'knockback', 'vm.login.dialog', 'jquery', 'user.settings'],
    function (ko, config, user, kb, login, $, userSettings) {

        return {
            currentUser: userSettings.currentUser,
            toggleSignUp: login.toggleSignUp
        };
    });