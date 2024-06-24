import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-numeric-keypad',
  templateUrl: './numeric-keypad.component.html',
  styleUrls: ['./numeric-keypad.component.css']
})
export class NumericKeypadComponent {
  @Output() numberPressed = new EventEmitter<number>();
  @Output() backspacePressed = new EventEmitter<void>();
  @Output() clearPressed = new EventEmitter<void>();

  appendNumber(number: number) {
    this.numberPressed.emit(number);
  }

  backspace() {
    this.backspacePressed.emit();
  }

  clear() {
    this.clearPressed.emit();
  }
}
