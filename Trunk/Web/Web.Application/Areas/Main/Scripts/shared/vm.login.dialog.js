define('vm.login.dialog',
    ['ko', 'config', 'kb', 'model.user', 'jquery', 'uri', 'bootstrap.helper'],
    function (ko, config, kb, user, $, uri, bh) {

        var signUpVisible = ko.observable(false),
            loginVisible = ko.observable(true),
            verificationPassword = ko.observable('');

        var toggleSignUp = function(toggleValue) {
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

        var signUpValidationOptions = ko.observable({
                debug: true,
                rules: {
                    username:
                    {
                        required: true,
                        minlength: 4,
                        maxlength: 10
                    },
                    signupPassword:
                    {
                        required: true,
                        minlength: 6,
                        maxlength: 12
                    },
                    retryPassword:
                    {
                        required: true
                    },
                    signupEmail: {
                        required: true,
                        email: true,
                        remote:
                        {
                            url: "/validate",
                            type: "post"
                        }
                    }
                },
                messages: {
                    username:
                    {
                        required: "username required",
                        minlength: "must be between 4 and 10 characters",
                        maxlength: "must be between 4 and 10 characters"
                    },
                    signupEmail: {
                        required: "email address required",
                        email: "not a valid email address",
                        remote: "email address is already in use"
                    },
                    signupPassword:
                    {
                        required: "password required",
                        minlength: "must be between 6 and 12 characters",
                        maxlength: "must be between 6 and 12 characters"
                    },
                    retryPassword: {
                        required: "verification password required",
                        equalTo: "passwords do not match"
                    }
                },
                submitHandler: function() {
                    alert('submitted!');
                },
                errorPlacement: bh.popoverErrorPlacement('login-popover'),
                unhighlight: bh.popoverUnhighlight
            });
            
        var loginValidationOptions = ko.observable({        
            rules: {
                loginPassword:
                {
                    required: true,
                },
                loginEmail: {
                    required: true,
                    email: true
                }
            },
            messages: {
                loginEmail: {
                    required: "email address required",
                    email: "not a valid email address"
                },
                loginPassword:
                {
                    required: "password required"
                }
            },
            submitHandler: function() {
                alert('submitted!');
            },
            errorPlacement: bh.popoverErrorPlacement('login-popover'),
            unhighlight: bh.popoverUnhighlight
        });

        return {
            toggleSignUp: toggleSignUp,
            signUpVisible: signUpVisible,
            loginVisible: loginVisible,
            signUpSwptUser: signUpSwptUser,
            signUpOAtuhUser: signUpOAtuhUser,
            loginSwptUser: loginSwptUser,
            verificationPassword: verificationPassword,
            signUpValidationOptions: signUpValidationOptions,
            loginValidationOptions: loginValidationOptions
        };

    });