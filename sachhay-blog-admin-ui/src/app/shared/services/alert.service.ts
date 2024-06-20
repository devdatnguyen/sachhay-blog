import {Injectable, inject} from '@angular/core';
import {MessageService} from 'primeng/api';

@Injectable()
export class AlertService {
  constructor(private messageService: MessageService) {}

  showSuccess(message: string) {
    this.messageService.add({severity: 'success', summary: 'Thành công', detail: message});
  }

  showError(message: string) {
    this.messageService.add({severity: 'error', summary: 'Lỗi', detail: message});
  }

  showInfo(message: string) {
    this.messageService.add({severity: 'info', summary: 'Info', detail: message});
  }

  showWarn(message: string) {
    this.messageService.add({severity: 'warn', summary: 'Warning', detail: message});
  }
}
