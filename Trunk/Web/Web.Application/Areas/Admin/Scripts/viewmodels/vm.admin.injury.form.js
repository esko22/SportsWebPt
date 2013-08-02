define('vm.admin.injury.form',
    ['ko', 'underscore', 'knockback', 'model.admin.injury', 'error.helper', 'bootstrap.helper', 'model.admin.plan.collection', 'model.admin.body.region.collection', 'model.admin.cause.collection', 'model.admin.sign.collection'],
    function (ko, _, kb, InjuryModel, err, bh, PlanCollection, BodyRegionCollection, CauseCollection, SignCollection) {
        var planCollection = new PlanCollection(),
           bodyRegionCollection = new BodyRegionCollection(),
           signCollection = new SignCollection(), 
           causeCollection = new CauseCollection(),
           availablePlans = kb.collectionObservable(planCollection),
           availableBodyRegions = kb.collectionObservable(bodyRegionCollection),
           availableSigns = kb.collectionObservable(signCollection),
           availableCauses = kb.collectionObservable(causeCollection),
           selectedInjury = new InjuryModel(),
           commonName = kb.observable(selectedInjury, 'commonName'),
           medicalName = kb.observable(selectedInjury, 'medicalName'),
           description = kb.observable(selectedInjury, 'description'),
           openingStatement = kb.observable(selectedInjury, 'openingStatement'),
           modalDialogId = '#admin-injury-form-dialog',
           tags = kb.observable(selectedInjury, 'tags'),
           pageName = kb.observable(selectedInjury, 'pageName'),
           plans = kb.collectionObservable(selectedInjury.get('plans'), availablePlans.shareOptions()),
           bodyRegions = kb.collectionObservable(selectedInjury.get('bodyRegions'), availableBodyRegions.shareOptions()),
           signs = kb.collectionObservable(selectedInjury.get('signs'), availableSigns.shareOptions()),
           causes = kb.collectionObservable(selectedInjury.get('causes'), availableCauses.shareOptions()),
           callback = function () {
               alert('test');
           },
          onSuccessfulChange = function () {
              if (callback) {
                  callback();
              }
              bindSelectedInjury(kb.viewModel(new InjuryModel()), null);
              $(modalDialogId).modal('hide');
          },
       saveChanges = function () {
           var injury = InjuryModel.findOrCreate(selectedInjury);
           injury.save({}, {
               success: onSuccessfulChange, error: err.onError
           });
       },
        addInjury = function (data, event) {
            $(modalDialogId).modal('show');
        },
        editInjury = function (data, event) {
            bindSelectedInjury(data);
            $(modalDialogId).modal('show');
        },
       suscribe = function (passedCallback) {
           callback = passedCallback;
       },
       bindSelectedInjury = function (data, event) {
           selectedInjury.get('plans').reset();
           selectedInjury.get('bodyRegions').reset();
           selectedInjury.get('signs').reset();
           selectedInjury.get('causes').reset();

           _.each(data.plans(), function (viewModel) {
               _.each(planCollection.models, function (planModel) {
                   if (viewModel.id() === planModel.get('id')) {
                       selectedInjury.get('plans').add(planModel);
                   }
               });
           });

           _.each(data.bodyRegions(), function (viewModel) {
               _.each(bodyRegionCollection.models, function (bodyRegionModel) {
                   if (viewModel.id() === bodyRegionModel.get('id')) {
                       selectedInjury.get('bodyRegions').add(bodyRegionModel);
                   }
               });
           });

           _.each(data.signs(), function (viewModel) {
               _.each(signCollection.models, function (signModel) {
                   if (viewModel.id() === signModel.get('id')) {
                       selectedInjury.get('signs').add(signModel);
                   }
               });
           });

           _.each(data.causes(), function (viewModel) {
               _.each(causeCollection.models, function (causeModel) {
                   if (viewModel.id() === causeModel.get('id')) {
                       selectedInjury.get('causes').add(causeModel);
                   }
               });
           });

           selectedInjury.set('commonName', data.model().get('commonName'));
           selectedInjury.set('description', data.model().get('description'));
           selectedInjury.set('medicalName', data.model().get('medicalName'));
           selectedInjury.set('openingStatement', data.model().get('openingStatement'));
           selectedInjury.set('tags', data.model().get('tags'));
           selectedInjury.set('pageName', data.model().get('pageName'));

           //TODO: fucking look into this... bb r-m complains of dual entity when you do a set on ('id')
           selectedInjury.id = data.model().get('id');

       },
       injuryValidationOptions = ko.observable({
           debug: true,
           rules: {
               commonName:
               {
                   required: true,
                   minlength: 4,
                   maxlength: 30
               },
               openingStatement:
               {
                   required: true,
                   minlength: 1,
                   maxlength: 2000
               },
               description:
               {
                   required: true,
                   minlength: 1,
                   maxlength: 1000
               },
               medicalName:
               {
                   required: true,
                   minlength: 1,
                   maxlength: 100
               },
               tags:
               {
                   minlength: 1,
                   maxlength: 1000
               },
               pageName:
               {
                   minlength: 4,
                   maxlength: 50
               }
           },
           messages: {
               commonName:
               {
                   required: "common name required",
                   minlength: "must be between 4 and 30 characters",
                   maxlength: "must be between 4 and 30 characters"
               },
               medicalName:
               {
                   required: "common name required",
                   minlength: "must be between 1 and 100 characters",
                   maxlength: "must be between 1 and 100 characters"
               },
               description:
               {
                   minlength: "must be between 1 and 1000 characters",
                   maxlength: "must be between 1 and 1000 characters"
               },
               openingStatement: {
                   required: "category must be set",
                   minlength: "must be between 1 and 2000 characters",
                    maxlength: "must be between 1 and 2000 characters"
                },
               tags: {
                   minlength: "must be between 1 and 1000 characters",
                   maxlength: "must be between 1 and 1000 characters"
               },
               pageName: {
                   remote: "page name must be unique",
                   minlength: "must be between 1 and 1000 characters",
                   maxlength: "must be between 1 and 1000 characters"
               }
           },
           submitHandler: saveChanges,
           errorPlacement: bh.popoverErrorPlacement('login-popover'),
           unhighlight: bh.popoverUnhighlight
       });

        var init = function() {
            planCollection.fetch();
            bodyRegionCollection.fetch();
            signCollection.fetch();
            causeCollection.fetch();
        };

        return {
            saveChanges: saveChanges,
            injuryValidationOptions: injuryValidationOptions,
            availablePlans: availablePlans,
            availableBodyRegions: availableBodyRegions,
            availableCauses: availableCauses,
            availableSigns: availableSigns,
            commonName: commonName,
            medicalName: medicalName,
            description: description,
            openingStatement: openingStatement,
            plans: plans,
            bodyRegions: bodyRegions,
            signs: signs,
            causes : causes,
            bindSelectedInjury: bindSelectedInjury,
            tags: tags,
            pageName: pageName,
            suscribe: suscribe,
            addInjury: addInjury,
            editInjury: editInjury,
            init : init
        };

    });