define('vm.injury.plan.display',
    ['knockback'],
    function (kb) {

        function ReseachInjuryPlanDisplay(displayPlan) {
            this.plan = kb.viewModel(displayPlan);

            if (typeof this.plan.exercises()[0] !== "undefined") {
                var exercise = displayPlan.get('exercises').models[0];
                this.selectedExercise = kb.viewModel(exercise);
            }
        }

        ReseachInjuryPlanDisplay.prototype.updateSelectedExercise = function (data) {
            this.selectedExercise.model(data.model());
        };

        return ReseachInjuryPlanDisplay;

    });
