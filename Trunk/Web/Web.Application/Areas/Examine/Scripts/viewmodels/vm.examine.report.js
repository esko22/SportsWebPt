define('vm.examine.report',
    ['ko', 'underscore', 'knockback'],
    function (ko, _, kb) {
        var injuries = kb.collectionObservable();
        var workouts = ko.observableArray();
        
        var bindReport = function(report) {
            injuries.collection(report.get('potentialInjuries'));
            _.each(report.get('potentialInjuries').models, function (injury) {
                _.each(injury.get('workouts').models, function (workout) {
                    workout.fetch({
                        success: function (data) {
                            workouts.push(kb.viewModel(data));
                        }
                    });
                });
            });
        };

        return {
            bindReport: bindReport,
            injuries: injuries,
            workouts: workouts
        };
    });