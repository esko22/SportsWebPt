﻿define('vm.admin.signs',
    ['ko', 'underscore', 'knockback', 'model.admin.sign.collection', 'model.admin.sign', 'error.helper', 'bootstrap.helper'],
    function (ko, _, kb, SignCollection, SignModel, err, bh) {

        var signCollection = new SignCollection(),
            signs = kb.collectionObservable(signCollection),
            selectedSign = kb.viewModel(new SignModel()),
            availableCategories = ko.observableArray(['Functional', 'Subjective', 'Visual']),
            bindSelectedSign = function (data, event) {
                var category = data.model().get('category');
                selectedSign.model(data.model());
                selectedSign.category(category);
            },
            onSuccessfulChange = function () {
                bindSelectedSign(kb.viewModel(new SignModel()), null);
                signCollection.fetch();
            },
            saveChanges = function () {
                selectedSign.model().save({}, {
                    success: onSuccessfulChange, error: err.onError
                });
            };


        var signValidationOptions = ko.observable({
            debug: true,
            rules: {
                description:
                {
                    required: false,
                    minlength: 1,
                    maxlength: 100
                },
                category:
                {
                    required: true,
                }
            },
            messages: {
                description:
                {
                    minlength: "must be between 1 and 100 characters",
                    maxlength: "must be between 1 and 100 characters"
                },
                category: {
                    required: "must select a category",
                }
            },
            submitHandler: saveChanges,
            errorPlacement: bh.popoverErrorPlacement('login-popover'),
            unhighlight: bh.popoverUnhighlight
        });

        var init = function() {
            signCollection.fetch();
        };
        
        return {
            signs: signs,
            selectedSign: selectedSign,
            saveChanges: saveChanges,
            bindSelectedSign: bindSelectedSign,
            signValidationOptions: signValidationOptions,
            availableCategories: availableCategories,
            init : init
        };
    });