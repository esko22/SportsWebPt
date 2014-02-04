define('admin.binder',
    ['jquery', 'knockback', 'logger', 'vm.admin.equipment', 'vm.admin.videos', 'vm.admin.exercise.page', 'vm.admin.plan.page',
        'vm.admin.signs', 'vm.admin.causes', 'vm.admin.injury.page', 'vm.admin.body.parts', 'vm.admin.body.regions', 'vm.admin.treatments', 'vm.admin.prognoses'],
    function ($, kb, logger, EquipmentVM, VideoVM, ExerciseVM, PlanVM, SignVM, CauseVM, InjuryVM, BodyPartVM, BodyRegionVM, TreatmentVM, PrognosisVM) {
        var bind = function() {
            logger.log('admin binding bound');

            ExerciseVM.bindViewModels();
            PlanVM.bindViewModels();
            InjuryVM.bindViewModels();
            kb.applyBindings(VideoVM, $('#admin-video-panel').get(0));
            kb.applyBindings(SignVM, $('#admin-sign-panel').get(0));
            kb.applyBindings(CauseVM, $('#admin-cause-panel').get(0));
            kb.applyBindings(EquipmentVM, $('#admin-equipment-panel').get(0));
            kb.applyBindings(BodyPartVM, $('#admin-body-part-panel').get(0));
            kb.applyBindings(BodyRegionVM, $('#admin-body-region-panel').get(0));
            kb.applyBindings(TreatmentVM, $('#admin-treatment-panel').get(0));
            kb.applyBindings(PrognosisVM, $('#admin-prognosis-panel').get(0));
        };

        return {
            bind: bind
        };
    });