import javax.swing.BorderFactory;
import javax.swing.ButtonGroup;
import javax.swing.GroupLayout;
import javax.swing.JCheckBox;
import javax.swing.JFileChooser;
import javax.swing.JFrame;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JProgressBar;
import javax.swing.JTextField;
import javax.swing.Timer;
import javax.swing.UIManager;
import javax.swing.border.Border;
import javax.swing.event.DocumentEvent;
import javax.swing.event.DocumentListener;
import javax.swing.filechooser.FileNameExtensionFilter;

import org.ini4j.Ini;

import java.awt.BorderLayout;
import java.awt.Container;
import java.awt.Dimension;
import java.awt.GridLayout;
import java.awt.Toolkit;
import java.awt.Color;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.ItemEvent;
import java.awt.event.ItemListener;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.PrintWriter;
import java.nio.file.Paths;

public class CE_Redef {
	static public String version = "1.1";
	static JFrame frame;
	private static Ini config;
	private static JProgressBar progress_bar;
	private static JButton run_button, ce_dir_button, backup_path_button;
	private static String ce_dir, backup_path, mode;
	private static boolean running = false;
	private static String cwd = "\\", ceVersion;
	private static double bulk_mult = -1234,
			carry_bulk_mult = -1234,
			worn_bulk_mult = -1234,
			reload_speed_mult = -1234,
			melee_cooldown_mult = -1234,
			warmup_mult = -1234,
			ranged_cooldown_mult = -1234,
			reload_time_mult = -1234;
	private static Timer timer;
	private static boolean started;
	private static JCheckBox backup_button, restore_button, redef_button;

	private static JTextField ce_dir_tf,
	backup_path_tf,
	bulk_tf,
	carry_bulk_tf,
	worn_bulk_tf,
	reload_speed_tf,
	reload_time_tf,
	ranged_cooldown_tf,
	melee_speed_tf,
	warmup_tf;
	private static Thread executor_thread;
	private static Execute executor;
	private static ValidityChecker val_checker;

	public static void main(String[] args) {
		try{
			UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
		}catch(Exception e){}
		try{
			Color text_color = new Color(255,255,255);
			Color bg_color = new Color(25, 35, 45);
			Color border_color = new Color(69, 83, 100);

			cwd = Paths.get("").toAbsolutePath().toString() + "\\CE Redef\\";
			new File(cwd).mkdirs();
			Border border = BorderFactory.createLineBorder(border_color);
			if(!new File(cwd + "config.ini").exists()){
				new File(cwd + "config.ini").createNewFile();
			}
			config = new Ini(new File(cwd + "config.ini"));

			frame = new JFrame("CE Redef v" + version);
			frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
			Dimension scr_size = Toolkit.getDefaultToolkit().getScreenSize();
			int w = scr_size.width / 2;
			int h = scr_size.height / 2;
			int x = (scr_size.width - w) / 2;
			int y = (scr_size.height - h) / 2;
			frame.setBounds(x, y, w, h);
			Container content = frame.getContentPane();
			content.setBackground(bg_color);
			GroupLayout content_layout = new GroupLayout(content);
			content_layout.setAutoCreateGaps(true);
			content_layout.setAutoCreateContainerGaps(true);
			content.setLayout(content_layout);

			// Create directory inputs
			JPanel dir_panel = new JPanel();
			dir_panel.setBorder(border);
			dir_panel.setBackground(bg_color);
			GroupLayout dir_layout = new GroupLayout(dir_panel);
			dir_layout.setAutoCreateGaps(true);
			dir_layout.setAutoCreateContainerGaps(true);
			dir_panel.setLayout(dir_layout);

			ce_dir_button = new JButton("Combat Extended Dir");
			backup_path_button = new JButton("Backup Path");

			ce_dir = getOrDefault(config, "dirs", "cedir", "C:\\Program Files\\Steam\\steamapps\\workshop\\content\\294100\\1631756268\\");
			backup_path = getOrDefault(config, "dirs", "backuppath", new File(cwd + "backup.ini").getAbsolutePath());
			ce_dir = fixPath(ce_dir, true);
			backup_path = fixPath(backup_path, false);

			ce_dir_tf = new JTextField(ce_dir);
			backup_path_tf = new JTextField(backup_path);

			dir_layout.setHorizontalGroup(
					dir_layout.createSequentialGroup()
					.addGroup(
							dir_layout.createParallelGroup(GroupLayout.Alignment.LEADING)
							.addComponent(ce_dir_button)
							.addComponent(backup_path_button)
							)
							.addGroup(
									dir_layout.createParallelGroup(GroupLayout.Alignment.TRAILING)
									.addComponent(ce_dir_tf)
									.addComponent(backup_path_tf)
									)
					);
			dir_layout.setVerticalGroup(
					dir_layout.createSequentialGroup()
					.addGroup(
							dir_layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
							.addComponent(ce_dir_button)
							.addComponent(ce_dir_tf)
							)
							.addGroup(
									dir_layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
									.addComponent(backup_path_button)
									.addComponent(backup_path_tf)
									)
					);

			// Add functionality to directory inputs
			ce_dir_button.addActionListener(new ActionListener() {
				public void actionPerformed(ActionEvent actionEvent){
					try{
						String path = getOrDefault(config, "prog data", "ce", ce_dir);
						if(!new File(path).exists()){
							config.put("prog data", "ce", cwd);
							config.store();
							path = cwd;
						}
						JFileChooser fc = new JFileChooser(path);
						fc.setFileSelectionMode(JFileChooser.DIRECTORIES_ONLY);
						fc.setMultiSelectionEnabled(false);
						int returnValue = fc.showOpenDialog(null);
						if(returnValue == JFileChooser.APPROVE_OPTION){
							File selection = fc.getSelectedFile();
							String new_path = selection.getAbsolutePath() + "\\";
							if(new File(new_path).exists() && new_path.endsWith("\\") && new File(new_path + "Patches\\").exists() && new File(new_path + "Defs\\").exists()){
								ceVersion = null;
								countFiles();
								ce_dir = new_path;
								config.put("prog data", "ce", ce_dir);
								config.put("dirs", "cedir", ce_dir);
								config.store();
								ce_dir_tf.setText(ce_dir);
								setRunButton();
							}
						}
					}catch(Exception e){
						e.printStackTrace();
					}
				}
			});

			backup_path_button.addActionListener(new ActionListener() {
				public void actionPerformed(ActionEvent actionEvent){
					try{
						String path = getOrDefault(config, "prog data", "backup", backup_path);
						if(!path.endsWith("\\")){
							path = removeFileName(path);
						}
						if(!new File(path).exists()){
							config.put("prog data", "backup", cwd);
							config.store();
							path = cwd;
						}
						JFileChooser fc = new JFileChooser(path);
						fc.setFileSelectionMode(JFileChooser.FILES_ONLY);
						fc.setMultiSelectionEnabled(false);
						fc.setAcceptAllFileFilterUsed(false);
						fc.addChoosableFileFilter(new FileNameExtensionFilter("*.ini", new String[] {"ini"}));
						int returnValue = fc.showOpenDialog(null);
						if(returnValue == JFileChooser.APPROVE_OPTION){
							File selection = fc.getSelectedFile();
							backup_path = selection.getAbsolutePath();
							config.put("prog data", "backup", backup_path);
							config.put("dirs", "backuppath", backup_path);
							config.store();
							backup_path_tf.setText(backup_path);
							setRunButton();
						}
					}catch(Exception e){
						e.printStackTrace();
					}
				}
			});

			ce_dir_tf.getDocument().addDocumentListener(new DocumentListener() {
				public void changedUpdate(DocumentEvent e){
					changed();
				}

				public void removeUpdate(DocumentEvent e){
					changed();
				}

				public void insertUpdate(DocumentEvent e){
					changed();
				}

				public void changed(){
					String text = ce_dir_tf.getText().replace('/', '\\');
					ce_dir = text;
					if(text.endsWith("\\")
							&& new File(text).exists()
							&& new File(text + "Patches\\").exists()
							&& new File(text + "Defs\\").exists()
							&& new File(text + "About\\Manifest.xml").exists()){
						ceVersion = null;
						config.put("dirs", "cedir", ce_dir);
						try{
							config.store();
						}catch(Exception e){
							e.printStackTrace();
						}
						countFiles();
					}
					setRunButton();
				}
			});

			backup_path_tf.getDocument().addDocumentListener(new DocumentListener() {
				public void changedUpdate(DocumentEvent e){
					changed();
				}

				public void removeUpdate(DocumentEvent e){
					changed();
				}

				public void insertUpdate(DocumentEvent e){
					changed();
				}

				public void changed(){
					String text = backup_path_tf.getText().replace('/', '\\');
					backup_path = text;
					if(text.endsWith(".ini")){
						config.put("dirs", "backuppath", text);
						try{
							config.store();
						}catch(Exception e){
							e.printStackTrace();
						}
					}
					setRunButton();
				}
			});

			// Create mode inputs
			JPanel mode_panel = new JPanel();
			mode_panel.setBorder(border);
			mode_panel.setBackground(bg_color);
			GridLayout mode_layout = new GridLayout(1, 3);
			mode_panel.setLayout(mode_layout);

			backup_button = new JCheckBox("Backup");
			restore_button = new JCheckBox("Restore");
			redef_button = new JCheckBox("Redefine");

			switch(getOrDefault(config, "mode", "mode", "backup")){
			default:
			case "backup":
				backup_button.setSelected(true);
				mode = "backup";
				break;

			case "restore":
				restore_button.setSelected(true);
				mode = "restore";
				break;

			case "redefine":
				redef_button.setSelected(true);
				mode = "redefine";
				break;
			}

			backup_button.setBackground(bg_color);
			restore_button.setBackground(bg_color);
			redef_button.setBackground(bg_color);

			backup_button.setForeground(text_color);
			restore_button.setForeground(text_color);
			redef_button.setForeground(text_color);

			ButtonGroup modeGroup = new ButtonGroup();
			modeGroup.add(backup_button);
			modeGroup.add(restore_button);
			modeGroup.add(redef_button);

			mode_panel.add(backup_button);
			mode_panel.add(restore_button);
			mode_panel.add(redef_button);

			// Add functionality to mode inputs
			backup_button.addItemListener(new ItemListener() {
				public void itemStateChanged(ItemEvent e) {
					if(e.getStateChange() == 1){
						mode = "backup";
						try{
							config.put("mode", "mode", mode);
							config.store();
						}catch(Exception E){
							E.printStackTrace();
						}
						setRunButton();
					}
				}
			});

			restore_button.addItemListener(new ItemListener() {
				public void itemStateChanged(ItemEvent e) {
					if(e.getStateChange() == 1){
						mode = "restore";
						try{
							config.put("mode", "mode", mode);
							config.store();
						}catch(Exception E){
							E.printStackTrace();
						}
						setRunButton();
					}
				}
			});

			redef_button.addItemListener(new ItemListener() {
				public void itemStateChanged(ItemEvent e) {
					if(e.getStateChange() == 1){
						mode = "redefine";
						try{
							config.put("mode", "mode", mode);
							config.store();
						}catch(Exception E){
							E.printStackTrace();
						}
						setRunButton();
					}
				}
			});

			// Create multiplier inputs
			JPanel mult_panel = new JPanel();
			mult_panel.setBorder(border);
			mult_panel.setBackground(bg_color);
			GroupLayout mult_layout = new GroupLayout(mult_panel);
			mult_layout.setAutoCreateGaps(true);
			mult_layout.setAutoCreateContainerGaps(true);
			mult_panel.setLayout(mult_layout);

			JLabel bulk_label = new JLabel("Bulk multiplier");
			JLabel carry_bulk_label = new JLabel("Carry bulk multiplier");
			JLabel worn_bulk_label = new JLabel("Worn bulk multiplier");
			JLabel reload_speed_label = new JLabel("Reload speed multiplier");
			JLabel reload_time_label = new JLabel("Reload time multiplier");
			JLabel ranged_cooldown_label = new JLabel("Ranged cooldown multiplier");
			JLabel melee_speed_label = new JLabel("Melee cooldown multiplier");
			JLabel warmup_label = new JLabel("Warmup multiplier");

			bulk_label.setForeground(text_color);
			carry_bulk_label.setForeground(text_color);
			worn_bulk_label.setForeground(text_color);
			reload_speed_label.setForeground(text_color);
			reload_time_label.setForeground(text_color);
			ranged_cooldown_label.setForeground(text_color);
			melee_speed_label.setForeground(text_color);
			warmup_label.setForeground(text_color);

			bulk_mult = getOrDefault(config, "multipliers", "bulkmult", 1.0);
			carry_bulk_mult = getOrDefault(config, "multipliers", "carrybulkmult", 1.0);
			worn_bulk_mult = getOrDefault(config, "multipliers", "wornbulkmult", 1.0);
			reload_speed_mult = getOrDefault(config, "multipliers", "reloadmult", 1.0);
			reload_time_mult = getOrDefault(config, "multipliers", "reloadtimemult", 1.0);
			ranged_cooldown_mult = getOrDefault(config, "multipliers", "rangedcooldownmult", 1.0);
			melee_cooldown_mult = getOrDefault(config, "multipliers", "meleespeedmult", 1.0);
			warmup_mult = getOrDefault(config, "multipliers", "warmupmult", 1.0);

			bulk_tf = new JTextField(bulk_mult+"");
			carry_bulk_tf = new JTextField(carry_bulk_mult+"");
			worn_bulk_tf = new JTextField(worn_bulk_mult+"");
			reload_speed_tf = new JTextField(reload_speed_mult+"");
			reload_time_tf = new JTextField(reload_time_mult+"");
			ranged_cooldown_tf = new JTextField(ranged_cooldown_mult+"");
			melee_speed_tf = new JTextField(melee_cooldown_mult+"");
			warmup_tf = new JTextField(warmup_mult+"");

			mult_layout.setHorizontalGroup(
					mult_layout.createSequentialGroup()
					.addGroup(
							mult_layout.createParallelGroup(GroupLayout.Alignment.LEADING)
							.addComponent(bulk_label)
							.addComponent(carry_bulk_label)
							.addComponent(worn_bulk_label)
							.addComponent(reload_speed_label)
							.addComponent(reload_time_label)
							.addComponent(ranged_cooldown_label)
							.addComponent(melee_speed_label)
							.addComponent(warmup_label)
							)
							.addGroup(
									mult_layout.createParallelGroup(GroupLayout.Alignment.TRAILING)
									.addComponent(bulk_tf)
									.addComponent(carry_bulk_tf)
									.addComponent(worn_bulk_tf)
									.addComponent(reload_speed_tf)
									.addComponent(reload_time_tf)
									.addComponent(ranged_cooldown_tf)
									.addComponent(melee_speed_tf)
									.addComponent(warmup_tf)
									)
					);
			mult_layout.setVerticalGroup(
					mult_layout.createSequentialGroup()
					.addGroup(
							mult_layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
							.addComponent(bulk_label)
							.addComponent(bulk_tf)
							)
							.addGroup(
									mult_layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
									.addComponent(carry_bulk_label)
									.addComponent(carry_bulk_tf)
									)
									.addGroup(
											mult_layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
											.addComponent(worn_bulk_label)
											.addComponent(worn_bulk_tf)
											)
											.addGroup(
													mult_layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
													.addComponent(reload_speed_label)
													.addComponent(reload_speed_tf)
													)
													.addGroup(
															mult_layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
															.addComponent(reload_time_label)
															.addComponent(reload_time_tf)
															)
															.addGroup(
																	mult_layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
																	.addComponent(ranged_cooldown_label)
																	.addComponent(ranged_cooldown_tf)
																	)
																	.addGroup(
																			mult_layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
																			.addComponent(melee_speed_label)
																			.addComponent(melee_speed_tf)
																			)
																			.addGroup(
																					mult_layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
																					.addComponent(warmup_label)
																					.addComponent(warmup_tf)
																					)
					);

			// Add functionality to multiplier inputs
			bulk_tf.getDocument().addDocumentListener(new DocumentListener() {
				public void changedUpdate(DocumentEvent e){
					changed();
				}

				public void removeUpdate(DocumentEvent e){
					changed();
				}

				public void insertUpdate(DocumentEvent e){
					changed();
				}

				public void changed(){
					String text = bulk_tf.getText();
					try{
						bulk_mult = Double.valueOf(text);
						config.put("multipliers", "bulkmult", bulk_mult);
						config.store();
					}catch(Exception e){
						bulk_mult = -1234;
					}
					setRunButton();
				}
			});

			carry_bulk_tf.getDocument().addDocumentListener(new DocumentListener() {
				public void changedUpdate(DocumentEvent e){
					changed();
				}

				public void removeUpdate(DocumentEvent e){
					changed();
				}

				public void insertUpdate(DocumentEvent e){
					changed();
				}

				public void changed(){
					String text = carry_bulk_tf.getText();
					try{
						carry_bulk_mult = Double.valueOf(text);
						config.put("multipliers", "carrybulkmult", carry_bulk_mult);
						config.store();
					}catch(Exception e){
						carry_bulk_mult = -1234;
					}
					setRunButton();
				}
			});

			worn_bulk_tf.getDocument().addDocumentListener(new DocumentListener() {
				public void changedUpdate(DocumentEvent e){
					changed();
				}

				public void removeUpdate(DocumentEvent e){
					changed();
				}

				public void insertUpdate(DocumentEvent e){
					changed();
				}

				public void changed(){
					String text = worn_bulk_tf.getText();
					try{
						worn_bulk_mult = Double.valueOf(text);
						config.put("multipliers", "wornbulkmult", worn_bulk_mult);
						config.store();
					}catch(Exception e){
						worn_bulk_mult = -1234;
					}
					setRunButton();
				}
			});

			reload_speed_tf.getDocument().addDocumentListener(new DocumentListener() {
				public void changedUpdate(DocumentEvent e){
					changed();
				}

				public void removeUpdate(DocumentEvent e){
					changed();
				}

				public void insertUpdate(DocumentEvent e){
					changed();
				}

				public void changed(){
					String text = reload_speed_tf.getText();
					try{
						reload_speed_mult = Double.valueOf(text);
						config.put("multipliers", "reloadmult", reload_speed_mult);
						config.store();
					}catch(Exception e){
						reload_speed_mult = -1234;
					}
					setRunButton();
				}
			});

			melee_speed_tf.getDocument().addDocumentListener(new DocumentListener() {
				public void changedUpdate(DocumentEvent e){
					changed();
				}

				public void removeUpdate(DocumentEvent e){
					changed();
				}

				public void insertUpdate(DocumentEvent e){
					changed();
				}

				public void changed(){
					String text = melee_speed_tf.getText();
					try{
						melee_cooldown_mult = Double.valueOf(text);
						config.put("multipliers", "meleespeedmult", melee_cooldown_mult);
						config.store();
					}catch(Exception e){
						melee_cooldown_mult = -1234;
					}
					setRunButton();
				}
			});

			warmup_tf.getDocument().addDocumentListener(new DocumentListener() {
				public void changedUpdate(DocumentEvent e){
					changed();
				}

				public void removeUpdate(DocumentEvent e){
					changed();
				}

				public void insertUpdate(DocumentEvent e){
					changed();
				}

				public void changed(){
					String text = warmup_tf.getText();
					try{
						warmup_mult = Double.valueOf(text);
						config.put("multipliers", "warmupmult", warmup_mult);
						config.store();
					}catch(Exception e){
						warmup_mult = -1234;
					}
					setRunButton();
				}
			});

			reload_time_tf.getDocument().addDocumentListener(new DocumentListener() {
				public void changedUpdate(DocumentEvent e){
					changed();
				}

				public void removeUpdate(DocumentEvent e){
					changed();
				}

				public void insertUpdate(DocumentEvent e){
					changed();
				}

				public void changed(){
					String text = reload_time_tf.getText();
					try{
						reload_time_mult = Double.valueOf(text);
						config.put("multipliers", "reloadtimemult", reload_time_mult);
						config.store();
					}catch(Exception e){
						reload_time_mult = -1234;
					}
					setRunButton();
				}
			});

			ranged_cooldown_tf.getDocument().addDocumentListener(new DocumentListener() {
				public void changedUpdate(DocumentEvent e){
					changed();
				}

				public void removeUpdate(DocumentEvent e){
					changed();
				}

				public void insertUpdate(DocumentEvent e){
					changed();
				}

				public void changed(){
					String text = ranged_cooldown_tf.getText();
					try{
						ranged_cooldown_mult = Double.valueOf(text);
						config.put("multipliers", "rangedcooldownmult", ranged_cooldown_mult);
						config.store();
					}catch(Exception e){
						ranged_cooldown_mult = -1234;
					}
					setRunButton();
				}
			});

			// Create run button
			JPanel run_panel = new JPanel(new BorderLayout());
			run_panel.setBackground(bg_color);
			JPanel run_panel2 = new JPanel();
			run_panel2.setBackground(bg_color);
			run_button = new JButton("Please wait");
			run_button.setEnabled(false);
			run_button.setPreferredSize(new Dimension(9999999, scr_size.height/41));
			run_panel2.add(run_button);
			run_panel.add(run_panel2);

			toggleInputs(false);
			
			// Add functionality to the run button
			run_button.addActionListener(new ActionListener() {
				public void actionPerformed(ActionEvent actionEvent){
					running = true;
					toggleInputs(false);
					setRunButton();
					executor = new Execute(getCEVersion(), config, ce_dir, backup_path, mode, bulk_mult, carry_bulk_mult, worn_bulk_mult, reload_speed_mult, reload_time_mult, ranged_cooldown_mult, melee_cooldown_mult, warmup_mult);
					executor_thread = new Thread(executor, "CE Redef Executor");
					started = false;
					timer = new Timer(1000/60,new ActionListener() {
						public void actionPerformed(ActionEvent e) {
							if(!started){
								started = true;
								executor_thread.start();
							}
							progress_bar.setValue(executor.file_index);
							frame.repaint();
							if(!executor.running){
								System.out.println("Done");
								timer.stop();
								progress_bar.setValue(progress_bar.getMaximum());
								running = false;
								toggleInputs(true);
								setRunButton();
							}
						}
					});
					timer.start();
				}
			});

			// Create progress display
			progress_bar = new JProgressBar();
			progress_bar.setStringPainted(true);

			// Create disclaimer
			JLabel disclaimer = new JLabel("If some weapons don't react to changes in these multipliers, they may not have the tags this looks for");
			disclaimer.setForeground(text_color);

			content_layout.setHorizontalGroup(
					content_layout.createParallelGroup(GroupLayout.Alignment.LEADING)
					.addComponent(dir_panel)
					.addComponent(mode_panel)
					.addComponent(mult_panel)
					.addComponent(run_panel)
					.addComponent(progress_bar)
					.addComponent(disclaimer)
					);
			content_layout.setVerticalGroup(
					content_layout.createSequentialGroup()
					.addComponent(dir_panel)
					.addComponent(mode_panel)
					.addComponent(mult_panel)
					.addComponent(run_panel)
					.addComponent(progress_bar)
					.addComponent(disclaimer)
					);

			frame.setVisible(true);
			countFiles();
			setRunButton();
			if(!running){
				toggleInputs(true);
			}
		}catch(Exception abc){}
	}
	
	private static void toggleInputs(boolean enabled){
		backup_button.setEnabled(enabled);
		restore_button.setEnabled(enabled);
		redef_button.setEnabled(enabled);
		ce_dir_button.setEnabled(enabled);
		ce_dir_tf.setEditable(enabled);
		backup_path_button.setEnabled(enabled);
		backup_path_tf.setEditable(enabled);
		bulk_tf.setEditable(enabled);
		worn_bulk_tf.setEditable(enabled);
		carry_bulk_tf.setEditable(enabled);
		reload_speed_tf.setEditable(enabled);
		reload_time_tf.setEditable(enabled);
		ranged_cooldown_tf.setEditable(enabled);
		melee_speed_tf.setEditable(enabled);
	}
	
	private static void countFiles(){
		Thread explorer_thread = new Thread(
				new TreeExplorer(ce_dir, progress_bar, frame),
				"CE Redef File Explorer"
				);
		explorer_thread.start();
	}

	private static String fixPath(String path, boolean is_dir){
		String new_path = path.replace('/', '\\').trim();
		if(new_path.equals("")){
			new_path = new File("").getAbsolutePath() + "\\";
		}
		if(!new File(new_path).exists()){
			new_path = cwd + "";
		}
		if(is_dir){
			if(!new_path.endsWith("\\")){
				new_path += '\\';
			}
		}else if(!new_path.endsWith(".ini")){
			new_path += "backup.ini";
		}
		return new_path;
	}

	private static double getOrDefault(Ini ini, String section, String key, double def) {
		String val = ini.get(section, key);
		if(val == null){
			ini.put(section, key, def);
			try{
				ini.store();
			}catch(Exception abc){}
			return def;
		}else{
			return Double.valueOf(val);
		}
	}

	private static String getOrDefault(Ini ini, String section, String key, String def) {
		String val = ini.get(section, key);
		if(val == null){
			ini.put(section, key, def);
			try{
				ini.store();
			}catch(Exception abc){}
			return def;
		}else{
			return val;
		}
	}

	private static void setRunButton(){
		if(val_checker != null){
			val_checker.stop = true;
		}
		val_checker = new ValidityChecker(running, mode, ce_dir, backup_path, getCEVersion(), bulk_mult, carry_bulk_mult, worn_bulk_mult, reload_speed_mult, reload_time_mult, ranged_cooldown_mult, melee_cooldown_mult, warmup_mult, run_button, frame);
		Thread thread = new Thread(val_checker, "CE Redef Validitity");
		thread.start();
	}

	public static void save(String file, String text){
		try{
			new File(file).delete();
			PrintWriter out = new PrintWriter(new File(file));
			out.print(text);
			out.close();
		}catch(Exception e){
			e.printStackTrace();
		}
	}

	private static String removeFileName(String path){
		return path.substring(0, path.lastIndexOf('\\')+1);
	}

	private static String getCEVersion(){
		if(ceVersion != null){
			return ceVersion;
		}
		String path = ce_dir + "About\\Manifest.xml";
		if(new File(path).exists()){

			String content = "";
			File f = new File(path);
			if(f.exists()){
				FileReader fr = null;
				BufferedReader br = null;
				try{
					fr = new FileReader(path);
					br = new BufferedReader(fr);
					int i;
					boolean stage = true;
					while((i=br.read())!=-1){
						if(stage){
							content += (char)i;
							if(content.endsWith("<version>")){
								content = "";
								stage = false;
							}
						}else if(i == 60){
							ceVersion = content; 
							return content;
						}else{
							content += (char)i;
						}
					}
				}catch(Exception e){System.out.println(e);}
				finally{
					try{
						if(fr!=null)fr.close();
						if(br!=null)br.close();
						fr = null;
						br = null;
					}catch(Exception E){System.out.println(E);}
				}
			}
		}
		return null;
	}
}
