define('vm.examine.description.symptom',
    ['ko'],
    function (ko) {

        var selectedAreas = ko.observableArray();

        return {
            selectedAreas: selectedAreas
        };
    });