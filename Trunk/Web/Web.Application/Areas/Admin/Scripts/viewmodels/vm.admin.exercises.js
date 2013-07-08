define('vm.admin.exercises',
    ['ko', 'underscore', 'knockback', 'model.admin.exercise.collection', 'model.admin.exercise', 'error.helper', 'bootstrap.helper', 'model.admin.video.collection', 'model.admin.equipment.collection'],
    function (ko, _, kb, ExerciseCollection, ExerciseModel, err, bh, VideoCollection, EquipmentCollection) {

        var exerciseListTemplate = 'admin.exercise.list',
            exerciseCollection = new ExerciseCollection(),
            videoCollection = new VideoCollection(),
            equipmentCollection = new EquipmentCollection(),
            exercises = kb.collectionObservable(exerciseCollection),
            videos = kb.collectionObservable(videoCollection),
            equipment = kb.collectionObservable(equipmentCollection),
            selectedExercise = kb.viewModel(new ExerciseModel()),
            bindSelectedExercise = function (data, event) {
                selectedExercise.model(data.model());
            },
            onSuccessfulChange = function () {
                selectedExercise.model(new ExerciseModel());
                exerciseCollection.fetch();
            },
            saveChanges = function () {
                selectedExercise.model().save({}, {
                    success: onSuccessfulChange, error: err.onError
                });
            };


        var exerciseValidationOptions = ko.observable({
            debug: true,
            rules: {
                name:
                {
                    required: true,
                    minlength: 4,
                    maxlength: 30
                },
                fileName:
                {
                    required: true,
                    minlength: 5,
                    maxlength: 40
                },
                description:
                {
                    required: false,
                    minlength: 1,
                    maxlength: 50
                },
                youtubeId:
                {
                    required: false,
                    minlength: 6,
                    maxlength: 20
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
                youtubeId: {
                    minlength: "must be between 6 and 20 characters",
                    maxlength: "must be between 6 and 20 characters"
                }
            },
            submitHandler: saveChanges,
            errorPlacement: bh.popoverErrorPlacement('login-popover'),
            unhighlight: bh.popoverUnhighlight
        });


        exerciseCollection.fetch();
        videoCollection.fetch();
        equipmentCollection.fetch();
        
        return {
            exercises: exercises,
            exerciseListTemplate: exerciseListTemplate,
            selectedExercise: selectedExercise,
            saveChanges: saveChanges,
            bindSelectedExercise: bindSelectedExercise,
            exerciseValidationOptions: exerciseValidationOptions,
            videos: videos,
            equipment: equipment
        };
    });