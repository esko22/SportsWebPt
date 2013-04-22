define('vm.examine.container',
    ['ko', 'vm.examine.description.area'],
    function (ko, description) {

        var selectedAreas = ko.observableArray();

        return {
            selectedAreas: selectedAreas
        };
    });