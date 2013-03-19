define('vm.login.dialog',
    ['ko', 'config', 'kb', 'model.user', 'jquery'],
    function (ko, config, kb, user, $) {
        
        var signUpVisible = ko.observable(false),
            loginVisible = ko.observable(true),
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
                loginVisible(false);
                signUpVisible(true);
            } else {
                loginVisible(true);
                signUpVisible(false);
            }
        },
            
        signUp = function() {
            config.currentUser().save();
        },

        signUpGoogle = function(onSuccess, onError) {
            window.location.href = '/oauth?returnUrl=examine';
        };
       

        return {
            toggleSignUp: toggleSignUp,
            signUpVisible: signUpVisible,
            loginVisible: loginVisible,
            signUp : signUp,
            emailAddress : emailAddress,
            password : password,
            username: username,
            activate: activate,
            signUpGoogle : signUpGoogle
        };

    });