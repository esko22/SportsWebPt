define('vm.admin.plan.form',
    ['ko', 'config', 'underscore', 'knockback', 'model.admin.plan', 'error.helper', 'bootstrap.helper', 'model.admin.exercise.collection', 'model.body.region.collection', 'model.admin.plan.exercise'],
    function (ko, config, _, kb, PlanModel, err, bh, ExerciseCollection, BodyRegionCollection, Exercise) {
        var exerciseCollection = new ExerciseCollection(),
            bodyRegionCollection = new BodyRegionCollection(),
            availableExercises = kb.collectionObservable(exerciseCollection),
            availableBodyRegions = kb.collectionObservable(bodyRegionCollection),
            selectedPlan = new PlanModel(),
            routineName = kb.observable(selectedPlan, 'routineName'),
            categories = kb.observable(selectedPlan, 'categories'),
            description = kb.observable(selectedPlan, 'description'),
            duration = kb.observable(selectedPlan, 'duration'),
            tags = kb.observable(selectedPlan, 'tags'),
            pageName = kb.observable(selectedPlan, 'pageName'),
            structuresInvolved = kb.observable(selectedPlan, 'structuresInvolved'),
            instructions = kb.observable(selectedPlan, 'instructions'),
            exercises = kb.collectionObservable(selectedPlan.get('exercises')),
            bodyRegions = kb.collectionObservable(selectedPlan.get('bodyRegions'), availableBodyRegions.shareOptions()),
            availableCategories = config.functionCategories,
            modalDialogId = '#admin-plan-form-dialog',
            rangeValues = ko.observableArray(),
            callback = function() {
                alert('test');
            },
            onSuccessfulChange = function() {
                if (callback) {
                    callback();
                }
                bindSelectedPlan(kb.viewModel(new PlanModel()), null);
                $(modalDialogId).modal('hide');
            },
            saveChanges = function() {
                var plan = PlanModel.findOrCreate(selectedPlan);
                plan.save({}, {
                    success: onSuccessfulChange,
                    error: err.onError
                });
            },
            suscribe = function(passedCallback) {
                callback = passedCallback;
            },
            addPlan = function(data, event) {
                $(modalDialogId).modal('show');
            },
            editPlan = function(data, event) {
                bindSelectedPlan(data);
                $(modalDialogId).modal('show');
            },
            addNewExercise = function() {
                var exercise = new Exercise();
                exercise.set('sets', 0);
                exercise.set('repititions', 0);
                exercise.set('exerciseId', 0);
                exercise.set('perWeek', 0);
                exercise.set('perDay', 0);
                exercise.set('holdFor', 0);
                exercise.set('holdType', 0);
                selectedPlan.get('exercises').add(exercise);
            },
            removeExercise = function(data, event) {
                selectedPlan.get('exercises').remove(data.model());
            },
            bindSelectedPlan = function(data, event) {

                selectedPlan.get('exercises').reset();
                selectedPlan.get('bodyRegions').reset();

                _.each(data.exercises(), function(viewModel) {
                    var exercise = viewModel.model();
                    selectedPlan.get('exercises').push(exercise);
                });

                _.each(data.bodyRegions(), function(viewModel) {
                    _.each(bodyRegionCollection.models, function(bodyRegionModel) {
                        if (viewModel.id() === bodyRegionModel.get('id')) {
                            selectedPlan.get('bodyRegions').add(bodyRegionModel);
                        }
                    });
                });

                selectedPlan.set('routineName', data.model().get('routineName'));
                selectedPlan.set('description', data.model().get('description'));
                selectedPlan.set('instructions', data.model().get('instructions'));
                selectedPlan.set('duration', data.model().get('duration'));
                selectedPlan.set('categories', data.model().get('categories'));
                selectedPlan.set('tags', data.model().get('tags'));
                selectedPlan.set('pageName', data.model().get('pageName'));
                selectedPlan.set('structuresInvolved', data.model().get('structuresInvolved'));


                ////TODO: fucking look into this... bb r-m complains of dual entity when you do a set on ('id')
                // think I got it with putting the findOrCreate in the save method which merges the 2
                selectedPlan.id = data.model().get('id');

            },
            planValidationOptions = ko.observable({
                debug: true,
                ignore: config.kendoEditorIgnores,
                rules: {
                    name:
                    {
                        required: true,
                        minlength: 4,
                        maxlength: 100
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
                        maxlength: 60000
                    },
                    instructions:
                    {
                        required: false,
                        minlength: 1,
                        maxlength: 60000
                    },
                    category:
                    {
                        required: true
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
                    name:
                    {
                        required: "name required",
                        minlength: "must be between 4 and 100 characters",
                        maxlength: "must be between 4 and 100 characters"
                    },
                    description:
                    {
                        minlength: "must be between 1 and 60000 characters",
                        maxlength: "must be between 1 and 60000 characters"
                    },
                    instructions:
                    {
                        minlength: "must be between 1 and 60000 characters",
                        maxlength: "must be between 1 and 60000 characters"
                    },
                    category: {
                        required: "category must be set"
                    },
                    tags: {
                        minlength: "must be between 1 and 1000 characters",
                        maxlength: "must be between 1 and 1000 characters"
                    },
                    pageName: {
                        required: "page name is required",
                        minlength: "must be between 4 and 50 characters",
                        maxlength: "must be between 4 and 50 characters"
                    }
                },
                submitHandler: saveChanges,
                errorPlacement: bh.popoverErrorPlacement('login-popover'),
                unhighlight: bh.popoverUnhighlight
            });


                var init = function() {

                    for (var i = 0; i <= 100; i++) {
                        rangeValues.push(i);
                    }
                    ;

                    bodyRegionCollection.fetch();
                    exerciseCollection.fetch();
                };
        
                return {
                    saveChanges: saveChanges,
                    planValidationOptions: planValidationOptions,
                    availableExercises: availableExercises,
                    availableBodyRegions: availableBodyRegions,
                    availableCategories: availableCategories,
                    routineName: routineName,
                    description: description,
                    duration: duration,
                    exercises: exercises,
                    bodyRegions: bodyRegions,
                    bindSelectedPlan: bindSelectedPlan,
                    tags: tags,
                    pageName: pageName,
                    suscribe: suscribe,
                    structuresInvolved: structuresInvolved,
                    addNewExercise: addNewExercise,
                    removeExercise: removeExercise,
                    addPlan: addPlan,
                    editPlan: editPlan,
                    init: init,
                    rangeValues: rangeValues,
                    instructions: instructions,
                    holdTypes: config.holdTypes,
                    categories: categories,
                    kendoEditorOptions: config.kendoEditorOptions
                };

    });