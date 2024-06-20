import * as yup from "yup";

const CreateAchievementSchema = yup.object().shape( {
    sportEventId: yup.number().required( 'Sport event is required' ),
    placeFinished: yup.string().nullable().optional(),
    description: yup.string().nullable().optional(),
} );

export default CreateAchievementSchema;