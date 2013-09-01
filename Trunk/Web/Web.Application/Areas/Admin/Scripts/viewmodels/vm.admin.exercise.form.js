define('vm.admin.exercise.form',
    ['ko', 'underscore', 'knockback', 'model.admin.exercise', 'error.helper', 'bootstrap.helper', 'model.admin.video.collection', 'model.admin.equipment.collection', 'model.admin.body.region.collection', 'config', 'model.admin.body.part.collection'],
    function (ko, _, kb, ExerciseModel, err, bh, VideoCollection, EquipmentCollection, BodyRegionCollection, config, BodyPartCollection) {

        var videoCollection = new VideoCollection(),
            equipmentCollection = new EquipmentCollection(),
            bodyRegionCollection = new BodyRegionCollection(),
            bodyPartCollection = new BodyPartCollection(),
            availableVideos = kb.collectionObservable(videoCollection),
            availableEquipment = kb.collectionObservable(equipmentCollection),
            availableBodyRegions = kb.collectionObservable(bodyRegionCollection),
            availableBodyParts = kb.collectionObservable(bodyPartCollection),
            selectedExercise = new ExerciseModel(),
            availableCategories = config.functionCategories,
            rangeValues = ko.observableArray(),
            name = kb.observable(selectedExercise, 'name'),
            difficulty = kb.observable(selectedExercise, 'difficulty'),
            description = kb.observable(selectedExercise, 'description'),
            duration = kb.observable(selectedExercise, 'duration'),
            tags = kb.observable(selectedExercise, 'tags'),
            medicalName = kb.observable(selectedExercise, 'medicalName'),
            pageName = kb.observable(selectedExercise, 'pageName'),
            sets = kb.observable(selectedExercise, 'sets'),
            repititions = kb.observable(selectedExercise, 'repititions'),
            perWeek = kb.observable(selectedExercise, 'perWeek'),
            perDay = kb.observable(selectedExercise, 'perDay'),
            holdFor = kb.observable(selectedExercise, 'holdFor'),
            category = kb.observable(selectedExercise, 'category'),
            videos = kb.collectionObservable(selectedExercise.get('videos'), availableVideos.shareOptions()),
            equipment = kb.collectionObservable(selectedExercise.get('equipment'), availableEquipment.shareOptions()),
            bodyRegions = kb.collectionObservable(selectedExercise.get('bodyRegions'), availableBodyRegions.shareOptions()),
            bodyParts = kb.collectionObservable(selectedExercise.get('bodyParts'), availableBodyParts.shareOptions()),
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
            selectedExercise.get('bodyParts').reset();

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
            
            _.each(exercise.bodyParts(), function (viewModel) {
                _.each(bodyPartCollection.models, function (bodyPartModel) {
                    if (viewModel.id() === bodyPartModel.get('id')) {
                        selectedExercise.get('bodyParts').add(bodyPartModel);
                    }
                });
            });

            selectedExercise.set('name', exercise.model().get('name'));
            selectedExercise.set('medicalName', exercise.model().get('medicalName'));
            selectedExercise.set('description', exercise.model().get('description'));
            selectedExercise.set('duration', exercise.model().get('duration'));
            selectedExercise.set('difficulty', exercise.model().get('difficulty'));
            selectedExercise.set('tags', exercise.model().get('tags'));
            selectedExercise.set('pageName', exercise.model().get('pageName'));
            selectedExercise.set('sets', exercise.model().get('sets'));
            selectedExercise.set('repititions', exercise.model().get('repititions'));
            selectedExercise.set('perWeek', exercise.model().get('perWeek'));
            selectedExercise.set('perDay', exercise.model().get('perDay'));
            selectedExercise.set('holdFor', exercise.model().get('holdFor'));
            selectedExercise.set('category', exercise.model().get('category'));


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
                    maxlength: 100
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
                    maxlength: 60000
                },
                difficulty:
                {
                    required: false
                },
                tags :
                {
                    minlength: 1,
                    maxlength: 10000
                },
                pageName :
                {
                    minlength: 4,
                    maxlength: 50,
                }
            },
            messages: {
                name:
                {
                    required: "name required",
                    minlength: "must be between 4 and 100 characters",
                    maxlength: "must be between 4 and 100 characters"
                },
                fileName: {
                    required: "file name required",
                    minlength: "must be between 5 and 40 characters",
                    maxlength: "must be between 5 and 40 characters"
                },
                description:
                {
                    minlength: "must be between 1 and 60000 characters",
                    maxlength: "must be between 1 and 60000 characters"
                },
                difficulty: {
                    required: "difficulty must be set"
                },
                tags: {
                    minlength: "must be between 1 and 10000 characters",
                    maxlength: "must be between 1 and 10000 characters"
                },
                pageName: {
                    remote: "page name must be unique",
                    minlength: "must be between 4 and 50 characters",
                    maxlength: "must be between 4 and 50 characters"
                }
            },
            submitHandler: saveChanges,
            errorPlacement: bh.popoverErrorPlacement('login-popover'),
            unhighlight: bh.popoverUnhighlight
        });

        var init = function() {
            for (var i = 1; i <= 100; i++) {
                rangeValues.push(i);
            };
            videoCollection.fetch();
            equipmentCollection.fetch();
            bodyRegionCollection.fetch();
            bodyPartCollection.fetch();
        };
        
        return {
            saveChanges: saveChanges,
            exerciseValidationOptions: exerciseValidationOptions,
            availableVideos: availableVideos,
            availableEquipment: availableEquipment,
            availableBodyRegions : availableBodyRegions,
            availableBodyParts: availableBodyParts,
            availableDifficulties: availableDifficulties,
            name : name,
            difficulty : difficulty,
            description : description,
            duration : duration,
            videos : videos,
            equipment: equipment,
            bodyRegions : bodyRegions,
            bodyParts: bodyParts,
            editExercise: editExercise,
            tags: tags,
            pageName: pageName,
            suscribe: suscribe,
            addExercise: addExercise,
            init: init,
            rangeValues: rangeValues,
            sets : sets,
            repititions : repititions,
            perWeek : perWeek,
            perDay : perDay,
            holdFor: holdFor,
            medicalName: medicalName,
            availableCategories: availableCategories,
            category: category
        };
    });