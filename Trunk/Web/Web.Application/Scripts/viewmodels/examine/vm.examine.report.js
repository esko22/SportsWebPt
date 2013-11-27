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
            });

            setTimeout(function() {
                //TODO: hack... dependent on the controller sorting desc by liklyhood
                setInjuryContent(injuries()[0].model());
                $('#injury-report-nav a:first').tab('show');
            }, 3000);
        };
        
        var postExerciseRender = function (elements) {
            // TODO: cannot figure out why this will not work... still pulling back the infuser template
            //$(elements[0]).next().addClass('active');
            $('div[id^="workout-plan-exercise-panes-"] > div:first-child').addClass('active');
        };

        var postRecoveryPlanRender = function (elements) {
            $(elements[0]).find('div[id^="workout-plan-exercises-"] > ul > :first-child').addClass('active');
        };

        function setInjuryContent(injury) {
            if ($('#' + injury.get('animationTag')).children().length == 0) {
                openthis = injury.get('animationTag');
                vm_open();
            }

            $('#recovery-plan-accordion a:first').collapse('show');
        }

        function onPillClick(data, event) {
            $("#injury-report-nav li.active").removeClass("active");
            setInjuryContent(data.model());
        }

        return {
            bindReport: bindReport,
            remoteInjuries: remoteInjuries,
            probabableInjuries: probabableInjuries,
            moderateInjuries: moderateInjuries,
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