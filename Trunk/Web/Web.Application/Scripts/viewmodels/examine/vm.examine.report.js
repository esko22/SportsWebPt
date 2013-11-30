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

        function setInjuryContent(injury) {
            if ($('#' + injury.get('animationTag')).children().length == 0) {
                openthis = injury.get('animationTag');
                vm_open();
            }

            _.each(injury.get('plans').models, function(plan) {
                if (plan.get('exercises').length === 0) {
                    plan.fetch();
                }
            });
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
            injuries: injuries,
            onPillClick : onPillClick
        };
    });