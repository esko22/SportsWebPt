define('vm.login.dialog',
    ['ko', 'config', 'kb', 'model.user', 'jquery', 'uri'],
    function (ko, config, kb, user, $, uri) {
        
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
            
        signUp = function () {

            //var token = $('input[name=""__RequestVerificationToken""]').val();
            //var headers = {};
            //headers['__RequestVerificationToken'] = token;
            //ajax ---- headers: headers
            
            config.currentUser().save();
        },

        signUpGoogle = function (onSuccess, onError) {

            var returnUri = location.pathname + location.search;
            var returnUriParams = new uri(location.search).getQueryParamValues('ReturnUrl');
            if (returnUriParams.length > 0) {
                returnUri = returnUriParams[0];
            } 
            
            window.location.href = '/oauth?provider=Google&returnUrl=' + returnUri;
        };
        
        signUpFacebook = function (onSuccess, onError) {

            var returnUri = location.pathname + location.search;
            var returnUriParams = new uri(location.search).getQueryParamValues('ReturnUrl');
            if (returnUriParams.length > 0) {
                returnUri = returnUriParams[0];
            }

            window.location.href = '/oauth?provider=Facebook&returnUrl=' + returnUri;
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
            signUpGoogle: signUpGoogle,
            signUpFacebook: signUpFacebook
        };

    });