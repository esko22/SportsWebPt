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
            
        signUpSwptUser = function () {

            //var token = $('input[name=""__RequestVerificationToken""]').val();
            //var headers = {};
            //headers['__RequestVerificationToken'] = token;
            //ajax ---- headers: headers
            
            config.currentUser().save();
        },
            
        loginSwptUser = function() {
            
        },

        signUpOAtuhUser = function (provider) {

            var returnUri = location.pathname + location.search;
            var returnUriParams = new uri(location.search).getQueryParamValues('ReturnUrl');
            if (returnUriParams.length > 0) {
                returnUri = returnUriParams[0];
            }

            window.location.href = $.format('/oauth?provider={0}&ReturnUrl={1}', provider, returnUri);
        };

        return {
            toggleSignUp: toggleSignUp,
            signUpVisible: signUpVisible,
            loginVisible: loginVisible,
            emailAddress : emailAddress,
            password : password,
            username: username,
            activate: activate,
            signUpSwptUser: signUpSwptUser,
            signUpOAtuhUser: signUpOAtuhUser,
            loginSwptUser: loginSwptUser
        };

    });