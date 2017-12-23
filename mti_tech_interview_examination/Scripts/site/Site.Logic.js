import { Api } from "./Site.Api";

export class Logic {
    render() {
        let api = new Api();
        api.loadQuestions(); 
    }
}