define('vm.login.dialog',
    ['ko', 'config', 'kb', 'model.user'],
    function (ko, config, kb, user) {
        
        var signUpVisible = ko.observable(false),
            signInVisible = ko.observable(false),
            emailAddress = kb.observable(null, 'emailAddress'),
            password = kb.observable(null, 'password'),
            username = kb.observable(null, 'userName');

        //functions
        var activate = function () {
            config.currentUser(new user());
            emailAddress.model(config.currentUser());
            password.model(config.currentUser());
            username.model(config.currentUser());
        },

        toggleSignUp = function (toggleValue) {
            if (toggleValue === 'signup') {
                signInVisible(false);
                signUpVisible(true);
            } else {
                signInVisible(true);
                signUpVisible(false);
            }
        },
            
        signUp = function() {
            config.currentUser().save();
        };

        return {
            toggleSignUp: toggleSignUp,
            signUpVisible: signUpVisible,
            signInVisible: signInVisible,
            signUp : signUp,
            emailAddress : emailAddress,
            password : password,
            username: username,
            activate : activate
        };

    });