define('vm.examine.container',
    ['ko', 'vm.examine.description.area'],
    function (ko, description) {

        var selectedAreas = ko.observableArray();

        var addSelectedArea = function(area) {

        };


        return {
            selectedAreas: selectedAreas
        };
    });