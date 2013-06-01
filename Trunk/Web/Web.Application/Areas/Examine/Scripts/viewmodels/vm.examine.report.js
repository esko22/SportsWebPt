define('vm.examine.report',
    ['ko', 'underscore', 'knockback'],
    function (ko, _, kb) {
        var injuries = kb.collectionObservable(),
            injuryTemplate = 'examine.report.injury',
            recoveryPlanTemplate = 'research.recovery.plans',
            researchWorkoutPlanTemplate = 'research.workout.plan',
            researchExerciseTemplate = 'research.exercise';

        var bindReport = function (report) {
            injuries.collection(report.get('potentialInjuries'));
            _.each(report.get('potentialInjuries').models, function (injury) {
                _.each(injury.get('workouts').models, function (workout) {
                    workout.fetch({
                        success: function (data) {
                            //workouts.push(kb.viewModel(data));
                        }
                    });
                });
            });
        };
        
        var postTabRender = function (elements) {
            $('#examine-report-tab-nav > :first-child').addClass('active');
            $('#examine-report-container > :first-child').addClass('active');
        };

        return {
            bindReport: bindReport,
            injuries: injuries,
            postTabRender: postTabRender,
            injuryTemplate: injuryTemplate,
            recoveryPlanTemplate: recoveryPlanTemplate,
            researchWorkoutPlanTemplate: researchWorkoutPlanTemplate,
            researchExerciseTemplate: researchExerciseTemplate
        };
    });