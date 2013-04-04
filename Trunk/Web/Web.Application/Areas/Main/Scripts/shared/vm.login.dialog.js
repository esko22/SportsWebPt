define('vm.login.dialog',
    ['ko', 'config', 'kb', 'model.user', 'jquery', 'uri'],
    function (ko, config, kb, user, $, uri) {
        
        var signUpVisible = ko.observable(false),
            loginVisible = ko.observable(true),
            emailAddress = kb.observable(null, 'emailAddress'),
            password = kb.observable(null, 'password'),
            verificationPassword = ko.observable(''),
            username = kb.observable(null, 'userName');

        //functions
        var activate = function() {
            config.currentUser(new user());
            emailAddress.model(config.currentUser());
            password.model(config.currentUser());
            username.model(config.currentUser());
        },
            toggleSignUp = function(toggleValue) {
                if (toggleValue === 'signup') {
                    loginVisible(false);
                    signUpVisible(true);
                } else {
                    loginVisible(true);
                    signUpVisible(false);
                }
            },
            signUpSwptUser = function() {

                //var token = $('input[name=""__RequestVerificationToken""]').val();
                //var headers = {};
                //headers['__RequestVerificationToken'] = token;
                //ajax ---- headers: headers

                //config.currentUser().save();
            },
            loginSwptUser = function() {

            },
            signUpOAtuhUser = function(provider) {

                var returnUri = location.pathname + location.search;
                var returnUriParams = new uri(location.search).getQueryParamValues('ReturnUrl');
                if (returnUriParams.length > 0) {
                    returnUri = returnUriParams[0];
                }

                window.location.href = $.format('/oauth?provider={0}&ReturnUrl={1}', provider, returnUri);
            };

        $('#signUpSwpt').validate(
        {
            debug: true,
            rules: {
                username: "required",
                signupPassword: "required",
                retryPassword:
                {
                    required: true
                },
                signupEmail: {
                    required: true,
                    email: true
                }
            },
            messages: {
                username: "username reqd" ,
                signupEmail: {
                    required: "email reqd",
                    email: "no email"
                },
                signupPassword: "required",
                retryPassword: {
                    required: "verify password required verify password required verify password required",
                    equalTo: "passwords not equal"
                }
            },
            submitHandler: function() {
                alert('submitted!');
            },
            errorPlacement: function (error, element) {
                $(element).popover({
                    animation: true,
                    trigger: 'manual',
                    content: error.text(),
                    template: '<div class="popover"><div class="arrow"></div><div class="popover-inner login-popover"><h3 class="popover-title"></h3><div class="popover-content"><p></p></div></div></div>'
                });
                $(element).popover('show');
            },
            unhighlight: function(element) {
                $(element).popover('hide');
            } 
                
        });

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
            loginSwptUser: loginSwptUser,
            verificationPassword: verificationPassword
            //validationOptions: validationOptions
        };

    });