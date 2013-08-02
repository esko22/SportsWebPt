define('vm.admin.exercise.form',
    ['ko', 'underscore', 'knockback', 'model.admin.exercise', 'error.helper', 'bootstrap.helper', 'model.admin.video.collection', 'model.admin.equipment.collection', 'model.admin.body.region.collection'],
    function (ko, _, kb, ExerciseModel, err, bh, VideoCollection, EquipmentCollection, BodyRegionCollection) {

        var videoCollection = new VideoCollection(),
            equipmentCollection = new EquipmentCollection(),
            bodyRegionCollection = new BodyRegionCollection(),
            availableVideos = kb.collectionObservable(videoCollection),
            availableEquipment = kb.collectionObservable(equipmentCollection),
            availableBodyRegions = kb.collectionObservable(bodyRegionCollection),
            selectedExercise = new ExerciseModel(),
            name = kb.observable(selectedExercise, 'name'),
            difficulty = kb.observable(selectedExercise, 'difficulty'),
            description = kb.observable(selectedExercise, 'description'),
            duration = kb.observable(selectedExercise, 'duration'),
            tags = kb.observable(selectedExercise, 'tags'),
            pageName = kb.observable(selectedExercise, 'pageName'),
            videos = kb.collectionObservable(selectedExercise.get('videos'), availableVideos.shareOptions()),
            equipment = kb.collectionObservable(selectedExercise.get('equipment'), availableEquipment.shareOptions()),
            bodyRegions = kb.collectionObservable(selectedExercise.get('bodyRegions'), availableBodyRegions.shareOptions()),
            availableDifficulties = ko.observableArray(['Beginner', 'Intermediate', 'Advanced']),
            modalDialogId = '#admin-exercise-form-dialog',
            callback = function() {
                alert('test');
            },
           onSuccessfulChange = function() {
             if (callback) {
                 callback();
             }
             bindSelectedExercise(kb.viewModel(new ExerciseModel()), null);
             $(modalDialogId).modal('hide');
           },
        saveChanges = function () {
            var exercise = ExerciseModel.findOrCreate(selectedExercise);
            exercise.save({}, {
                success: onSuccessfulChange, error: err.onError
            });
        },
        suscribe = function(passedCallback) {
            callback = passedCallback;
        },
        addExercise = function(data, event) {
            
            $(modalDialogId).modal('show');
        },
        editExercise = function(data, event) {
            bindSelectedExercise(data);
            $(modalDialogId).modal('show');
        },    
        bindSelectedExercise = function (exercise) {

            selectedExercise.get('equipment').reset();
            selectedExercise.get('videos').reset();
            selectedExercise.get('bodyRegions').reset();

            //have to get items from available collection
            _.each(exercise.equipment(), function (viewModel) {
                _.each(equipmentCollection.models, function (equipmentModel) {
                    if (viewModel.id() === equipmentModel.get('id')) {
                        selectedExercise.get('equipment').add(equipmentModel);
                    }
                });
            });
            
            _.each(exercise.videos(), function (viewModel) {
                _.each(videoCollection.models, function (videoModel) {
                    if (viewModel.id() === videoModel.get('id')) {
                        selectedExercise.get('videos').add(videoModel);
                    }
                });
            });

            _.each(exercise.bodyRegions(), function (viewModel) {
                _.each(bodyRegionCollection.models, function (bodyRegionModel) {
                    if (viewModel.id() === bodyRegionModel.get('id')) {
                        selectedExercise.get('bodyRegions').add(bodyRegionModel);
                    }
                });
            });

            selectedExercise.set('name', exercise.model().get('name'));
            selectedExercise.set('description', exercise.model().get('description'));
            selectedExercise.set('duration', exercise.model().get('duration'));
            selectedExercise.set('difficulty', exercise.model().get('difficulty'));
            selectedExercise.set('tags', exercise.model().get('tags'));
            selectedExercise.set('pageName', exercise.model().get('pageName'));

            //TODO: fucking look into this... bb r-m complains of dual entity when you do a set on ('id')
            selectedExercise.id = exercise.model().get('id');
        },
        exerciseValidationOptions = ko.observable({
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
                    required: true,
                    digits: true,
                    maxlength: 2
                },
                description:
                {
                    required: true,
                    minlength: 1,
                    maxlength: 50
                },
                difficulty:
                {
                    required: false
                },
                tags :
                {
                    minlength: 1,
                    maxlength: 1000
                },
                pageName :
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
                fileName: {
                    required: "file name required",
                    minlength: "must be between 5 and 40 characters",
                    maxlength: "must be between 5 and 40 characters"
                },
                description:
                {
                    minlength: "must be between 1 and 50 characters",
                    maxlength: "must be between 1 and 50 characters"
                },
                difficulty: {
                    required: "difficulty must be set"
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
            videoCollection.fetch();
            equipmentCollection.fetch();
            bodyRegionCollection.fetch();
        };
        
        return {
            saveChanges: saveChanges,
            exerciseValidationOptions: exerciseValidationOptions,
            availableVideos: availableVideos,
            availableEquipment: availableEquipment,
            availableBodyRegions : availableBodyRegions,
            availableDifficulties: availableDifficulties,
            name : name,
            difficulty : difficulty,
            description : description,
            duration : duration,
            videos : videos,
            equipment: equipment,
            bodyRegions : bodyRegions,
            editExercise: editExercise,
            tags: tags,
            pageName: pageName,
            suscribe: suscribe,
            addExercise: addExercise,
            init : init
        };
    });