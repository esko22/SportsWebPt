define('vm.admin.exercises',
    ['ko', 'underscore', 'knockback', 'model.admin.exercise.collection'],
    function (ko, _, kb, ExerciseCollection) {

        var exerciseListTemplate = 'admin.exercise.list',
            self = this,
            exerciseCollection = new ExerciseCollection(),
            exercises = kb.collectionObservable(exerciseCollection),
            bindSelectedExercise = function (data, event) {


            //selectedExercise = {
            //    name: kb.observable(data.model(), 'name'),
            //    difficulty: kb.observable(data.model(), 'difficulty'),
            //    description: kb.observable(data.model(), 'description'),
            //    duration: kb.observable(data.model(), 'duration'),
            //    videos: kb.collectionObservable(data.model().get('videos'), availableEquipment.shareOptions())
            //};


            ////TODO: not sure why this observables don't get triggered on new set
            //kb.viewModel(new ExerciseModel())
            //selectedExercise.model(new ExerciseModel());
            //selectedExercise.model(data.model());
            //selectedExercise.difficulty(data.difficulty());

            //var findSelectedVideos = new VideoCollection();

            //_.each(data.videos(), function (selectedVideo) {
            //    var hit = _.find(videos(), function (video) {
            //        return video.id() === selectedVideo.id();
            //    });

            //    findSelectedVideos.push(hit.model());
            //});

            //selectedExercise.videos = new kb.collectionObservable(findSelectedVideos);
            //data.model().set({ "videos": findSelectedVideos });


            //selectedExercise.model(data.model());
        },
        onSuccessfulChangeCallback = function () {
            self.exerciseCollection.fetch();
        };


        exerciseCollection.fetch();
        
        return {
            exercises: exercises,
            exerciseListTemplate: exerciseListTemplate,
            bindSelectedExercise: bindSelectedExercise,
            onSuccessfulChangeCallback: onSuccessfulChangeCallback
        };
    });