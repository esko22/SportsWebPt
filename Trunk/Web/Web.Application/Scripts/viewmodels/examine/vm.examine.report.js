define('vm.examine.report',
    ['config', 'underscore', 'knockback'],
    function (config, _, kb) {
        var remoteInjuries = kb.collectionObservable(),
            probabableInjuries = kb.collectionObservable(),
            moderateInjuries = kb.collectionObservable(),
            injuries = kb.collectionObservable(),
            injuryTemplate = 'examine.report.injury',
            recoveryPlanTemplate = 'research.recovery.plans',
            researchWorkoutPlanTemplate = 'research.workout.plan',
            researchExerciseTemplate = 'research.exercise',
            researchVideoTemplate = 'research.video';

        var bindReport = function (report) {
            _.each(report.get('potentialInjuries').models, function (injury) {
                var injuryViewModel = kb.viewModel(injury);
                injuries.push(injuryViewModel);
                
                if (injury.get('likelyHood') >= config.likelyHoodThresholds.high)
                    probabableInjuries.push(injuryViewModel);
                else if (injury.get('likelyHood') >= config.likelyHoodThresholds.medium)
                    moderateInjuries.push(injuryViewModel);
                else
                    remoteInjuries.push(injuryViewModel);

                _.each(injury.get('plans').models, function (plan) {
                    plan.fetch();
                });
            });
        };
        
        var postTabRender = function (elements) {
            $('#examine-report-tab-nav > :first-child').addClass('active');
            $('#examine-report-container > :first-child').addClass('active');
        };
        
        var postExerciseRender = function (elements) {
            // TODO: cannot figure out why this will not work... still pulling back the infuser template
            //$(elements[0]).next().addClass('active');
            $('div[id^="workout-plan-exercise-panes-"] > div:first-child').addClass('active');
        };

        var postRecoveryPlanRender = function (elements) {
            $(elements[0]).find('div[id^="workout-plan-exercises-"] > ul > :first-child').addClass('active');
        };

        function onPillClick(event, data) {
            $("li.active").removeClass("active");
        }

        return {
            bindReport: bindReport,
            remoteInjuries: remoteInjuries,
            probabableInjuries: probabableInjuries,
            moderateInjuries: moderateInjuries,
            postTabRender: postTabRender,
            injuryTemplate: injuryTemplate,
            recoveryPlanTemplate: recoveryPlanTemplate,
            researchWorkoutPlanTemplate: researchWorkoutPlanTemplate,
            researchExerciseTemplate: researchExerciseTemplate,
            postExerciseRender: postExerciseRender,
            researchVideoTemplate: researchVideoTemplate,
            postRecoveryPlanRender: postRecoveryPlanRender,
            injuries: injuries,
            onPillClick : onPillClick
        };
    });