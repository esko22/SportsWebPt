define('vm.admin.injury.form',
    ['ko','config', 'underscore', 'knockback', 'model.admin.injury', 'error.helper', 'bootstrap.helper', 'model.admin.plan.collection', 'model.admin.body.region.collection', 'model.admin.cause.collection', 'model.admin.sign.collection', 'model.admin.symptom.collection','model.admin.body.part.matrix.item.collection', 'model.injury.symptom'],
    function (ko,config, _, kb, InjuryModel, err, bh, PlanCollection, BodyRegionCollection, CauseCollection, SignCollection, SymptomCollection, BodyPartMatrixCollection, InjurySymptom) {
        var planCollection = new PlanCollection(),
            bodyRegionCollection = new BodyRegionCollection(),
            signCollection = new SignCollection(),
            causeCollection = new CauseCollection(),
            symptomCollection = new SymptomCollection(),
            bodyPartMatrixCollection = new BodyPartMatrixCollection(),
           availablePlans = kb.collectionObservable(planCollection),
           availableBodyRegions = kb.collectionObservable(bodyRegionCollection),
           availableSigns = kb.collectionObservable(signCollection),
           availableCauses = kb.collectionObservable(causeCollection),
           availableSymptoms = kb.collectionObservable(symptomCollection),
            availableBodyPartMatrix = kb.collectionObservable(bodyPartMatrixCollection),
           selectedInjury = new InjuryModel(),
            injurySymptoms = kb.collectionObservable(selectedInjury.get('injurySymptoms')),
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
           rangeValues = ko.observableArray(),
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
        addNewSymptom = function (data, event) {
            var symptom = new InjurySymptom();
            symptom.set('symptomId', 1);
            symptom.set('bodyPartMatrixItemId', 1);
            symptom.set('renderTemplate', symptomCollection.models[0].get('renderTemplate'));
            //HACK
            symptom.set('id', 99999 + selectedInjury.get('injurySymptoms').length);
            selectedInjury.get('injurySymptoms').add(symptom);
        },
        removeSymptom = function (data, event) {
            selectedInjury.get('injurySymptoms').remove(data.model());
        },
       suscribe = function (passedCallback) {
           callback = passedCallback;
       },
       symptomSelectionChanged = function (symptom) {
           symptom.renderTemplate(symptomCollection.findWhere({ id: symptom.symptomId() }).get('renderTemplate'));
           symptom.givenResponse(0);
       },
       bindSelectedInjury = function (data, event) {
           selectedInjury.get('plans').reset();
           selectedInjury.get('bodyRegions').reset();
           selectedInjury.get('signs').reset();
           selectedInjury.get('causes').reset();
           selectedInjury.get('injurySymptoms').reset();
           
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
           
           _.each(data.injurySymptoms(), function (viewModel) {
                   selectedInjury.get('injurySymptoms').add(viewModel.model());
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
           ignore: config.kendoEditorIgnores,
           rules: {
               commonName:
               {
                   required: true,
                   minlength: 4,
                   maxlength: 100
               },
               openingStatement:
               {
                   required: true,
                   minlength: 1,
                   maxlength: 60000
               },
               description:
               {
                   required: true,
                   minlength: 1,
                   maxlength: 60000
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
                   maxlength: 10000
               },
               pageName:
               {
                   required: true,
                   minlength: 4,
                   maxlength: 50
               }
           },
           messages: {
               commonName:
               {
                   required: "common name required",
                   minlength: "must be between 4 and 100 characters",
                   maxlength: "must be between 4 and 100 characters"
               },
               medicalName:
               {
                   required: "medical name required",
                   minlength: "must be between 1 and 100 characters",
                   maxlength: "must be between 1 and 100 characters"
               },
               description:
               {
                   required: "description must be set",
                   minlength: "must be between 1 and 60000 characters",
                   maxlength: "must be between 1 and 60000 characters"
               },
               openingStatement: {
                   required: "opening statement must be set",
                   minlength: "must be between 1 and 60000 characters",
                    maxlength: "must be between 1 and 60000 characters"
                },
               tags: {
                   minlength: "must be between 1 and 10000 characters",
                   maxlength: "must be between 1 and 10000 characters"
               },
               pageName: {
                   required: "page name required", 
                   minlength: "must be between 4 and 50 characters",
                   maxlength: "must be between 4 and 50 characters"
               }
           },
           submitHandler: saveChanges,
           errorPlacement: bh.popoverErrorPlacement('login-popover'),
           unhighlight: bh.popoverUnhighlight
       });

        var init = function () {
            for (var i = 1; i <= 100; i++) {
                rangeValues.push(i);
            };

            planCollection.fetch();
            bodyRegionCollection.fetch();
            signCollection.fetch();
            causeCollection.fetch();
            bodyPartMatrixCollection.fetch();
            symptomCollection.fetch();
        };

        return {
            saveChanges: saveChanges,
            injuryValidationOptions: injuryValidationOptions,
            availablePlans: availablePlans,
            availableBodyRegions: availableBodyRegions,
            availableCauses: availableCauses,
            availableSigns: availableSigns,
            availableSymptoms: availableSymptoms,
            availableBodyPartMatrix: availableBodyPartMatrix,
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
            init: init,
            addNewSymptom: addNewSymptom,
            removeSymptom: removeSymptom,
            injurySymptoms: injurySymptoms,
            rangeValues: rangeValues,
            symptomSelectionChanged: symptomSelectionChanged,
            kendoEditorOptions: config.kendoEditorOptions
        };

    });