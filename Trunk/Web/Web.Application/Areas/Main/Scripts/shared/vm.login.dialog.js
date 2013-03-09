define('vm.login.dialog',
    ['ko', 'config', 'kb'],
    function (ko, config, kb) {
        
        var signUpVisible = ko.observable(false);
        var signInVisible = ko.observable(false);

        var toggleSignUp = function (toggleValue) {
            if (toggleValue === 'signup') {
                signInVisible(false);
                signUpVisible(true);
            } else {
                signInVisible(true);
                signUpVisible(false);
            }
        };

        return {
            toggleSignUp: toggleSignUp,
            signUpVisible: signUpVisible,
            signInVisible: signInVisible
        };

    });