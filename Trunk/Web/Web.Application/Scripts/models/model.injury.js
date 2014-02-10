define('model.injury', ['backbone', 'config', 'model.injury.plan', 'model.cause', 'model.sign', 'model.potential.symptom', 'model.body.region', 'model.treatment', 'model.injury.prognosis'],
    function (backbone, config, Plan, Cause, Sign, Symptom, BodyRegion, Treatment, InjuryPrognosis) {
        var
            injury = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.injuries,
                defaults: {
                    'commonName': '',
                    'medicalName': '',
                    'openingStatement': '',
                    'description': '',
                    'recovery': '',
                    'prognosis' : '',
                    'animationTag': '',
                    'pageName': ''
                },
                relations: [{
                    type: backbone.HasMany,
                    key: 'plans',
                    relatedModel: Plan,
                },
                {
                    type: backbone.HasMany,
                    key: 'causes',
                    relatedModel: Cause
                },
                {
                    type: backbone.HasMany,
                    key: 'lifestyleCauses',
                    relatedModel: Cause
                },
                {
                    type: backbone.HasMany,
                    key: 'physiologicalCauses',
                    relatedModel: Cause
                },
                {
                    type: backbone.HasMany,
                    key: 'signs',
                    relatedModel: Sign
                },
                {
                    type: backbone.HasMany,
                    key: 'treatments',
                    relatedModel: Treatment
                },
                {
                    type: backbone.HasMany,
                    key: 'bodyRegions',
                    relatedModel: BodyRegion
                },
                {
                    type: backbone.HasMany,
                    key: 'givenSymptoms',
                    relatedModel: Symptom
                },
                {
                    type: backbone.HasMany,
                    key: 'injuryPrognoses',
                    relatedModel: InjuryPrognosis
                },
                {
                    type: backbone.HasOne,
                    key: 'bestCase',
                    relatedModel: InjuryPrognosis
                },
                {
                    type: backbone.HasOne,
                    key: 'delayedRecovery',
                    relatedModel: InjuryPrognosis
                },
                {
                    type: backbone.HasOne,
                    key: 'worstCase',
                    relatedModel: InjuryPrognosis
                }]
            });

        return injury;

    });

define('model.injury.collection', ['backbone', 'model.injury', 'config'],
    function (backbone, component, config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.injuries
            });

        return componentCollection;

    });

define('model.admin.injury', ['backbone', 'config', 'model.admin.plan', 'model.admin.cause', 'model.admin.sign', 'model.admin.body.region','model.injury.symptom', 'model.admin.treatment', 'model.injury.prognosis'],
    function (backbone, config, Plan, Cause, Sign, BodyRegion, InjurySymptom, Treatment, InjuryPrognosis) {
        var
            injury = backbone.RelationalModel.extend({
                urlRoot: config.apiUris.adminInjuries,
                defaults : {
                },
                relations: [{
                    type: backbone.HasMany,
                    key: 'plans',
                    relatedModel: Plan
                },
                {
                    type: backbone.HasMany,
                    key: 'causes',
                    relatedModel: Cause
                },
                {
                    type: backbone.HasMany,
                    key: 'signs',
                    relatedModel: Sign
                },
                {
                    type: backbone.HasMany,
                    key: 'treatments',
                    relatedModel: Treatment
                },
                {
                    type: backbone.HasMany,
                    key: 'bodyRegions',
                    relatedModel: BodyRegion
                },
                {
                    type: backbone.HasMany,
                    key: 'injurySymptoms',
                    relatedModel: InjurySymptom
                },
                {
                    type: backbone.HasMany,
                    key: 'injuryPrognoses',
                    relatedModel: InjuryPrognosis
                }]
            });

        return injury;

    });

define('model.admin.injury.collection', ['backbone', 'model.admin.injury', 'config'],
    function (backbone, component, config) {
        var
            componentCollection = backbone.Collection.extend({
                model: component,
                url: config.apiUris.adminInjuries
            });

        return componentCollection;

    });