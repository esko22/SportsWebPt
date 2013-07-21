define('vm.research.injury',
    ['jquery', 'knockback', 'model.injury', 'underscore'],
    function($, kb, Injury, _) {

        var injuryModel = new Injury(JSON.parse($('#selected-injury').val())),
        injury = kb.viewModel(injuryModel);
        
        return {
            injury : injury
        };
    });