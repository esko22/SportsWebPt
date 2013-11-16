define('vm.research.locate',
    ['jquery', 'knockback','ko', 'model.clinic.collection', 'config'],
    function ($, kb, ko, ClinicCollection, config) {

        var clinicCollection = new ClinicCollection(),
            clinics = kb.collectionObservable(clinicCollection),
            zipcode = ko.observable(),
            onSearchSubmit = function() {
                clinicCollection.url = config.apiUris.clinics.replace('{zipcode}', zipcode());
                clinicCollection.fetch();
            };
        
        function init() {
            
        }

        return {
            clinics: clinics,
            zipcode: zipcode,
            onSearchSubmit: onSearchSubmit,
            init: init
        };
    });