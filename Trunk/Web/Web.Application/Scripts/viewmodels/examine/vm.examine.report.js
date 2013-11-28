define('vm.examine.report',
    ['config', 'underscore', 'knockback', 'backbone'],
    function (config, _, kb, backbone) {
        var remoteInjuries = kb.collectionObservable(),
            probabableInjuries = kb.collectionObservable(),
            moderateInjuries = kb.collectionObservable(),
            injuries = kb.collectionObservable();
;

        var bindReport = function (report) {
            injuries.collection(report.get('potentialInjuries'));

            _.each(injuries(), function(injury) {
                if (injury.likelyHood() >= config.likelyHoodThresholds.high)
                    probabableInjuries.push(injury);
                else if (injury.likelyHood() >= config.likelyHoodThresholds.medium)
                    moderateInjuries.push(injury);
                else
                    remoteInjuries.push(injury);
            });

            //TODO: hack... dependent on the controller sorting desc by liklyhood
            setInjuryContent(injuries()[0].model());
            $('#injury-report-nav a:first').tab('show');
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

            _.each(injury.get('plans').models, function(plan) {
                if (plan.get('exercises').length === 0) {
                    plan.fetch({ success: onSuccessfulPlanFetch(plan.get('id'), injury.get('id')) });
                }
            });

            //TODO this timeout is ghetto... works for now... 
            //sometimes div has not been dymaically created, post render not always working either
            //need to do more invesitagion
            setTimeout(function() {
                $('#workout-plan-' + injury.get('plans').models[0].get('id') + '-' + injury.get('id')).collapse('show');
            }, 1000);
        }

        function onSuccessfulPlanFetch(planId, injuryId) {
            //TODO this timeout is ghetto... works for now... 
            setTimeout(function () {
                $('#workout-plan-exercises-' + planId + '-' + injuryId + ' a:first').tab('show');
            }, 1000);
        }

        function onPillClick(data, event) {
            $("#injury-report-nav li.active").removeClass("active");
            setInjuryContent(data.model());

            setTimeout(function() {
                sublime.load();
            },10000);
        }

        return {
            bindReport: bindReport,
            remoteInjuries: remoteInjuries,
            probabableInjuries: probabableInjuries,
            moderateInjuries: moderateInjuries,
            postExerciseRender: postExerciseRender,
            postRecoveryPlanRender: postRecoveryPlanRender,
            injuries: injuries,
            onPillClick : onPillClick
        };
    });