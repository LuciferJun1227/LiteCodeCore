import { USER_GET, USER_SIGNIN } from './mutation-types'
export default {
    [USER_GET]: (state) => {
        return state["user"] || {};
    }
}
