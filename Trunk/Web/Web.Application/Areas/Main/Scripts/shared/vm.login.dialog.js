define('vm.login.dialog',
    ['ko', 'config', 'kb', 'model.user'],
    function (ko, config, kb, user) {
        
        var signUpVisible = ko.observable(false);
        var signInVisible = ko.observable(false);

        var userToAdd = new user();
        var emailAddress = kb.observable(userToAdd,'emailAddress');
        var password = kb.observable(userToAdd, 'password');
        var username = kb.observable(userToAdd, 'userName');

        var toggleSignUp = function (toggleValue) {
            if (toggleValue === 'signup') {
                signInVisible(false);
                signUpVisible(true);
            } else {
                signInVisible(true);
                signUpVisible(false);
            }
        };

        var signUp = function() {
            userToAdd.save();            
        };

        return {
            toggleSignUp: toggleSignUp,
            signUpVisible: signUpVisible,
            signInVisible: signInVisible,
            signUp : signUp,
            emailAddress : emailAddress,
            password : password,
            username : username
        };

    });