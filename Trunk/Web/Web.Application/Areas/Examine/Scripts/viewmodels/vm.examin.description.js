define('vm.examine.description',
    ['vm.examine.container'],
    function (container) {

        container.selectedAreas.subscribe(function (newVal) {
            alert(newVal.length);
        });

        return {
            selectedAreas: container.selectedAreas
        };
    });