CREATE DATABASE Gestao_Escolar;
use Gestao_Escolar;
CREATE TABLE Turma (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nome VARCHAR(100) NOT NULL
);

CREATE TABLE Materia (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nome VARCHAR(100) NOT NULL
);

CREATE TABLE Funcionario (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nome VARCHAR(100) NOT NULL,
  cpf VARCHAR(14) NOT NULL UNIQUE,
  cargo ENUM('professor', 'administrativo') NOT NULL,
  status ENUM('ativo', 'demitido') NOT NULL
);

CREATE TABLE Aluno (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nome VARCHAR(100) NOT NULL,
  data_nascimento DATE NOT NULL,
  foto_url VARCHAR(255),
  turma_id INT,
  status_matricula ENUM('ativo', 'inativo') NOT NULL,
  saldo_sonhos INT DEFAULT 0,
  FOREIGN KEY (turma_id) REFERENCES Turma(id)
);

CREATE TABLE Evento (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nome VARCHAR(100) NOT NULL,
  descricao TEXT,
  valor_sonhos INT NOT NULL,
  data_inicio DATETIME NOT NULL,
  data_fim DATETIME NOT NULL,
  status ENUM('ativo', 'encerrado') NOT NULL
);

CREATE TABLE ParticipacaoEvento (
  id INT AUTO_INCREMENT PRIMARY KEY,
  aluno_id INT NOT NULL,
  evento_id INT NOT NULL,
  participou BOOLEAN DEFAULT FALSE,
  FOREIGN KEY (aluno_id) REFERENCES Aluno(id),
  FOREIGN KEY (evento_id) REFERENCES Evento(id)
);

CREATE TABLE Chamada (
  id INT AUTO_INCREMENT PRIMARY KEY,
  funcionario_id INT NOT NULL,
  turma_id INT NOT NULL,
  materia_id INT NOT NULL,
  data DATE NOT NULL,
  FOREIGN KEY (funcionario_id) REFERENCES Funcionario(id),
  FOREIGN KEY (turma_id) REFERENCES Turma(id),
  FOREIGN KEY (materia_id) REFERENCES Materia(id)
);

CREATE TABLE ChamadaAluno (
  id INT AUTO_INCREMENT PRIMARY KEY,
  chamada_id INT NOT NULL,
  aluno_id INT NOT NULL,
  status ENUM('presente', 'ausente') NOT NULL,
  FOREIGN KEY (chamada_id) REFERENCES Chamada(id),
  FOREIGN KEY (aluno_id) REFERENCES Aluno(id)
);

CREATE TABLE HistoricoSonhos (
  id INT AUTO_INCREMENT PRIMARY KEY,
  aluno_id INT NOT NULL,
  data DATETIME NOT NULL,
  tipo ENUM('adição', 'subtração', 'uso_evento') NOT NULL,
  motivo VARCHAR(255),
  valor INT NOT NULL,
  funcionario_id INT,
  FOREIGN KEY (aluno_id) REFERENCES Aluno(id),
  FOREIGN KEY (funcionario_id) REFERENCES Funcionario(id)
);

CREATE TABLE TransferenciaTurma (
  id INT AUTO_INCREMENT PRIMARY KEY,
  aluno_id INT NOT NULL,
  turma_origem_id INT,
  turma_destino_id INT,
  data_transferencia DATE NOT NULL,
  funcionario_id INT,
  FOREIGN KEY (aluno_id) REFERENCES Aluno(id),
  FOREIGN KEY (turma_origem_id) REFERENCES Turma(id),
  FOREIGN KEY (turma_destino_id) REFERENCES Turma(id),
  FOREIGN KEY (funcionario_id) REFERENCES Funcionario(id)
);

CREATE TABLE Matricula (
  id INT AUTO_INCREMENT PRIMARY KEY,
  aluno_id INT NOT NULL,
  ano_letivo INT NOT NULL,
  status ENUM('ativa', 'cancelada', 'transferida') NOT NULL,
  data_inicio DATE NOT NULL,
  data_fim DATE,
  funcionario_id INT,
  FOREIGN KEY (aluno_id) REFERENCES Aluno(id),
  FOREIGN KEY (funcionario_id) REFERENCES Funcionario(id)
);