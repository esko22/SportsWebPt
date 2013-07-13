define('vm.admin.exercise.form',
    ['ko', 'underscore', 'knockback', 'model.admin.exercise', 'error.helper', 'bootstrap.helper', 'model.admin.video.collection', 'model.admin.equipment.collection'],
    function (ko, _, kb, ExerciseModel, err, bh, VideoCollection, EquipmentCollection) {

        var videoCollection = new VideoCollection(),
            equipmentCollection = new EquipmentCollection(),
            availableVideos = kb.collectionObservable(videoCollection),
            availableEquipment = kb.collectionObservable(equipmentCollection),
            selectedExercise = new ExerciseModel(),
            name = kb.observable(selectedExercise, 'name'),
            difficulty = kb.observable(selectedExercise, 'difficulty'),
            description = kb.observable(selectedExercise, 'description'),
            duration = kb.observable(selectedExercise, 'duration'),
            videos = kb.collectionObservable(selectedExercise.get('videos'), availableVideos.shareOptions()),
            equipment = kb.collectionObservable(selectedExercise.get('equipment'), availableEquipment.shareOptions()),
            availableDifficulties = ko.observableArray(['Beginner', 'Intermediate', 'Advanced']),

       
        onSuccessfulChange = function () {
            alert('need to refresh other guy');
        },
        saveChanges = function () {
            selectedExercise.save({}, {
                success: onSuccessfulChange, error: err.onError
            });
        };
        bindSelectedExercise = function (data, event) {

            selectedExercise.get('equipment').reset();
            selectedExercise.get('videos').reset();

            //have to get items from available collection
            _.each(data.equipment(), function (viewModel) {
                _.each(equipmentCollection.models, function (equipmentModel) {
                    if (viewModel.id() === equipmentModel.get('id')) {
                        selectedExercise.get('equipment').add(equipmentModel);
                    }
                });
            });
            
            _.each(data.videos(), function (viewModel) {
                _.each(videoCollection.models, function (videoModel) {
                    if (viewModel.id() === videoModel.get('id')) {
                        selectedExercise.get('videos').add(videoModel);
                    }
                });
            });

            selectedExercise.set('name', data.model().get('name'));
            selectedExercise.set('description', data.model().get('description'));
            selectedExercise.set('duration', data.model().get('duration'));
            selectedExercise.set('difficulty', data.model().get('difficulty'));

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
                }
            },
            submitHandler: saveChanges,
            errorPlacement: bh.popoverErrorPlacement('login-popover'),
            unhighlight: bh.popoverUnhighlight
        });


        videoCollection.fetch();
        equipmentCollection.fetch();
        
        return {
            saveChanges: saveChanges,
            exerciseValidationOptions: exerciseValidationOptions,
            availableVideos: availableVideos,
            availableEquipment: availableEquipment,
            availableDifficulties: availableDifficulties,
            name : name,
            difficulty : difficulty,
            description : description,
            duration : duration,
            videos : videos,
            equipment : equipment,
            bindSelectedExercise: bindSelectedExercise
        };
    });