import { LoadingController, ToastController } from 'ionic-angular';
import { Component } from '@angular/core';

@Component({})
export class BasePageComponent {
    
    public defaultLoadingMessage: string = "Carregando ...";    
    public defaultErrorMessage: string = "Ocorreu uma falha de comunicação com o servidor, tente novamente mais tarde.";

    public loadingView: any;
    public loadingQueue: number = 0;

    constructor(public loadingProvider: LoadingController, 
                public toastProvider: ToastController) { }
    
    
    //Controles de loading
    showLoading() {
        this.loadingView = this.loadingView ? this.loadingView : this.loadingProvider.create( { content: this.defaultLoadingMessage } );
        this.loadingQueue++;
        return this.loadingView.present();
    }

    dismissLoading() {
        if(this.loadingQueue > 0)
            this.loadingQueue--;

        if(this.loadingQueue === 0){
            this.loadingView.dismiss();
            this.loadingView = null;
        } 
    }
    /////////////////////////////////////////////////////////////////


    //Controles de toast
    showToast(text: string, duration?: number, position?: string) {
        let toast = this.toastProvider.create({ message: text, duration: duration ? duration : 3000, position: position ? position : 'bottom' });
        toast.present();
    }
    /////////////////////////////////////////////////////////////////


    //Controles de error
    handleAsGenericError() {
        this.showToast(this.defaultErrorMessage);
        this.dismissLoading();
    }
    /////////////////////////////////////////////////////////////////

    
}