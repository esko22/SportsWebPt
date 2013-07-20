﻿define('vm.admin.plan.form',
    ['ko', 'underscore', 'knockback', 'model.admin.plan', 'error.helper', 'bootstrap.helper', 'model.admin.exercise.collection', 'model.admin.body.region.collection'],
    function (ko, _, kb, PlanModel, err, bh, ExerciseCollection, BodyRegionCollection) {
            var exerciseCollection = new ExerciseCollection(),
               bodyRegionCollection = new BodyRegionCollection(),
               availableExercises = kb.collectionObservable(exerciseCollection),
               availableBodyRegions = kb.collectionObservable(bodyRegionCollection),
               selectedPlan = new PlanModel(),
               routineName = kb.observable(selectedPlan, 'routineName'),
               category = kb.observable(selectedPlan, 'category'),
               description = kb.observable(selectedPlan, 'description'),
               duration = kb.observable(selectedPlan, 'duration'),
               tags = kb.observable(selectedPlan, 'tags'),
               pageName = kb.observable(selectedPlan, 'pageName'),
               musclesInvolved = kb.observable(selectedPlan, 'musclesInvolved'),
               exercises = kb.collectionObservable(selectedPlan.get('exercises'), availableExercises.shareOptions()),
               bodyRegions = kb.collectionObservable(selectedPlan.get('bodyRegions'), availableBodyRegions.shareOptions()),
               availableCategories = ko.observableArray(['Rehabilitation', 'Streching', 'Preventative']),
               callback = function () {
                   alert('test');
               },
              onSuccessfulChange = function () {
                  if (callback) {
                      callback();
                  }
                  bindSelectedPlan(kb.viewModel(new PlanModel()), null);
              },
           saveChanges = function () {
               selectedPlan.save({}, {
                   success: onSuccessfulChange, error: err.onError
               });
           },
           suscribe = function (passedCallback) {
               callback = passedCallback;
           },
           bindSelectedPlan = function (data, event) {

               selectedPlan.get('exercises').reset();
               selectedPlan.get('bodyRegions').reset();

               //have to get items from available collection
               _.each(data.exercises(), function (viewModel) {
                   _.each(exerciseCollection.models, function (exerciseModel) {
                       if (viewModel.id() === exerciseModel.get('id')) {
                           selectedPlan.get('exercises').add(exerciseModel);
                       }
                   });
               });

               _.each(data.bodyRegions(), function (viewModel) {
                   _.each(bodyRegionCollection.models, function (bodyRegionModel) {
                       if (viewModel.id() === bodyRegionModel.get('id')) {
                           selectedPlan.get('bodyRegions').add(bodyRegionModel);
                       }
                   });
               });

               selectedPlan.id = data.model().get('id');
               selectedPlan.set('routineName', data.model().get('routineName'));
               selectedPlan.set('description', data.model().get('description'));
               selectedPlan.set('duration', data.model().get('duration'));
               selectedPlan.set('category', data.model().get('category'));
               selectedPlan.set('tags', data.model().get('tags'));
               selectedPlan.set('pageName', data.model().get('pageName'));
               selectedPlan.set('musclesInvolved', data.model().get('musclesInvolved'));

           },
           planValidationOptions = ko.observable({
               debug: true,
               rules: {
                   name:
                   {
                       required: true,
                       minlength: 4,
                       maxlength: 30
                   },
                   duration:
                   {
                       required: false,
                       digits: true,
                       maxlength: 2
                   },
                   description:
                   {
                       required: true,
                       minlength: 1,
                       maxlength: 500
                   },
                   category:
                   {
                       required: true
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
                   name:
                   {
                       required: "name required",
                       minlength: "must be between 4 and 30 characters",
                       maxlength: "must be between 4 and 30 characters"
                   },
                   description:
                   {
                       minlength: "must be between 1 and 50 characters",
                       maxlength: "must be between 1 and 50 characters"
                   },
                   category: {
                       required: "category must be set"
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

                exerciseCollection.fetch();
                bodyRegionCollection.fetch();

                return {
                    saveChanges: saveChanges,
                    planValidationOptions: planValidationOptions,
                    availableExercises: availableExercises,
                    availableBodyRegions: availableBodyRegions,
                    availableCategories: availableCategories,
                    routineName: routineName,
                    category: category,
                    description: description,
                    duration: duration,
                    exercises: exercises,
                    bodyRegions: bodyRegions,
                    bindSelectedPlan: bindSelectedPlan,
                    tags: tags,
                    pageName: pageName,
                    suscribe: suscribe,
                    musclesInvolved: musclesInvolved
                };

    });