define('vm.injury.plan.display', ['knockback', 'model.plan.exercise', 'youtube.video.manager'],
    function (kb, PlanExercise, youtubeManager) {

        function ReseachInjuryPlanDisplay(displayPlan) {
            this.plan = kb.viewModel(displayPlan);
            this.selectedExercise = kb.viewModel(new PlanExercise());

            if (typeof this.plan.exercises()[0] !== "undefined") {
                this.updateSelectedExercise(kb.viewModel(displayPlan.get('exercises').models[0]));
            }
        }

        ReseachInjuryPlanDisplay.prototype.updateSelectedExercise = function (data) {
            this.selectedExercise.model(data.model());
            var video = data.model().get('videos').models[0];
            youtubeManager.addVideoInstance('ytplayer-' + this.plan.id() + '-' + video.get('id'));
            
        };

        return ReseachInjuryPlanDisplay;

    });
