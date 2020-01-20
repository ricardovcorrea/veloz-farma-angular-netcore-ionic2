export class GenericError implements Error {

    public code: number;
    public message: string;
    public name: string;
    public stack: any;

    constructor(code:number, stack?:any, name?:string){

        this.code = code;
        this.message = this.ERROR_MESSAGES[code] || "Ocorreu um erro!";
        this.name = name || "Generic Error";
        this.stack = stack || {};
    
    }

    private ERROR_MESSAGES = {
    
        401 : "Usuario e/ou senha incorretos!",
        500 : "Ocorreu uma falha de comunicação com o servidor!",
        501 : "Session without token!",
        502 : "Session without expires!"

    }

}